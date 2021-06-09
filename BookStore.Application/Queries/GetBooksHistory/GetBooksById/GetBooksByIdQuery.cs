using BookStore.Application.Dto;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory.GetBooksById
{
    public class GetBooksByIdQuery : IRequest<Response<BookDto>>
    {
        public int Id { get; set; }
    }
}