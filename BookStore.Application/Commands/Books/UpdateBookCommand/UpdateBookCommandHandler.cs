using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.Dto;
using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Services;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Commands.Books.UpdateBookCommand
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Response<BookDto>>
    {
        private readonly IGenericCrudRepository<Author> _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly IGenericEventRepository<LoggedEvent> _eventRepository;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IGenericCrudRepository<Author> authorRepository,
            IGenericEventRepository<LoggedEvent> eventRepository, IDateTimeService dateTimeService, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _authorRepository =
                authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookWithAuthorAsync(request.Id);
            if (book == null) throw new ApiException($"book wih id {request.Id} not found");

            var allHistoryList = new List<LoggedEvent>();
            var titleLoggedEvent = CheckIfTitleWasChanged(request, book);
            if (titleLoggedEvent != null)
            {
                allHistoryList.Add(titleLoggedEvent);
            }

            allHistoryList.AddRange(await UpdateBookWithNewlyAddedAuthors(request, book));
            allHistoryList.AddRange(await UpdateBookWithDeletedAuthors(request, book));

            _mapper.Map(request, book);
            _bookRepository.Update(book);
            var bookSavingRes = await _bookRepository.SaveChangesAsync();
            if (!bookSavingRes) return new Response<BookDto>("Operation failed!");

            await _eventRepository.AppendRangeAsync(allHistoryList);
            var eventSavingRes = await _eventRepository.SaveChangesAsync();
            var bookDto = _mapper.Map<BookDto>(book);
      
            return eventSavingRes ? new Response<BookDto>(bookDto) : new Response<BookDto>("Operation failed!");
        }

        private LoggedEvent CheckIfTitleWasChanged(UpdateBookCommand request, Book book)
        {
            var wasTitleChanged = book.Title != request.Title;
            if (!wasTitleChanged)
                return null;
            var description = $"Title was changed from {book.Title} to {request.Title}";
            return CreateLoggedEvent(book, "Title changed", description);

        }

        private async Task<List<LoggedEvent>> UpdateBookWithNewlyAddedAuthors(UpdateBookCommand request, Book book)
        {
            List<LoggedEvent> historyList=new List<LoggedEvent>();
            foreach (var author in request.Authors)
            {
                var wasNewAuthorAdded = book.Authors.Exists(x => x.Id == author.Id);
                if (!wasNewAuthorAdded)
                {
                    var newAuthor = await _authorRepository.GetAsync(author.Id);
                    if (newAuthor != null)
                    {
                        book.Authors.Add(newAuthor);
                        var description = $"New Author '{newAuthor.Name}' was added";
                            
                        historyList.Add(CreateLoggedEvent(book, "Author Added", description));
                    }
                }
            }

            return historyList;
        }

        private async Task<List<LoggedEvent>> UpdateBookWithDeletedAuthors(UpdateBookCommand request, Book book)
        {
            List<LoggedEvent> historyList = new List<LoggedEvent>();
            List<Author> deletedAuthors = new List<Author>();
            foreach (var bookAuthor in book.Authors)
            {
                var authorStillExist = request.Authors.Exists(x=>x.Id==bookAuthor.Id);
                if (!authorStillExist)
                {
                    var author = await _authorRepository.GetAsync(bookAuthor.Id);
                    if (author != null)
                    {
                        deletedAuthors.Add(author);
                    }
                }
            }

            foreach (var deletedAuthor in deletedAuthors)
            {
                book.Authors.Remove(deletedAuthor);
                var description = $"Author '{deletedAuthor.Name}' was removed";
                historyList.Add(CreateLoggedEvent(book, "Author removed", description));

            }

            return historyList;
        }
        private LoggedEvent CreateLoggedEvent(Book book, string actionName, string description)
        {
            return new LoggedEvent
            {
                Action = actionName,
                BookId = book.Id,
                Data = JsonSerializer.Serialize(book, options: new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                }),
                Description = description,
                TimeStamp = _dateTimeService.NowUtc
            };
        }
    }
}