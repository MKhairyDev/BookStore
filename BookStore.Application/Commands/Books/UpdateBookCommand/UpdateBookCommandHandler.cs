using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Application.Exceptions;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Application.Services;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Commands.Books.UpdateBookCommand
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Response<Book>>
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

        public async Task<Response<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
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
            if (!bookSavingRes) return new Response<Book>("Operation failed!");

            await _eventRepository.AppendRangeAsync(allHistoryList);
            var eventSavingRes = await _eventRepository.SaveChangesAsync();
            return eventSavingRes ? new Response<Book>(book) : new Response<Book>("Operation failed!");
        }

        private LoggedEvent CheckIfTitleWasChanged(UpdateBookCommand request, Book book)
        {
            var wasTitleChanged = book.Title != request.Title;
            if (!wasTitleChanged)
                return null;

            return new LoggedEvent()
            {
                Action = "Title changed",
                Data = JsonSerializer.Serialize(book),
                Description = $"Title was changed from {book.Title} to {request.Title}",
                TimeStamp = _dateTimeService.NowUtc
            };

        }

        private async Task<List<LoggedEvent>> UpdateBookWithNewlyAddedAuthors(UpdateBookCommand request, Book book)
        {
            List<LoggedEvent> historyList=new List<LoggedEvent>();
            foreach (var authorId in request.Authors)
            {
                var wasNewAuthorAdded = book.Authors.Exists(x => x.Id == authorId);
                if (!wasNewAuthorAdded)
                {
                    var newAuthor = await _authorRepository.GetAsync(authorId);
                    if (newAuthor != null)
                    {
                        book.Authors.Add(newAuthor);

                        historyList.Add(new LoggedEvent
                        {
                            Action = "Author Added",
                            Data = JsonSerializer.Serialize(book),
                            Description = $"New Author '{newAuthor.Name}' was added",
                            TimeStamp = _dateTimeService.NowUtc
                        });
                    }
                }
            }

            return historyList;
        }

        private async Task<List<LoggedEvent>> UpdateBookWithDeletedAuthors(UpdateBookCommand request, Book book)
        {
            List<LoggedEvent> historyList = new List<LoggedEvent>();
            foreach (var bookAuthor in book.Authors)
            {
                var authorStillExist = request.Authors.Contains(bookAuthor.Id);
                if (!authorStillExist)
                {
                    var author = await _authorRepository.GetAsync(bookAuthor.Id);
                    if (author != null)
                    {
                        book.Authors.Remove(author);
                        historyList.Add(new LoggedEvent
                        {
                            Action = "Author removed",
                            Data = JsonSerializer.Serialize(book),
                            Description = $"Author '{author.Name}' was removed",
                            TimeStamp = _dateTimeService.NowUtc
                        });
                    }
                }
            }

            return historyList;
        }
    }
}