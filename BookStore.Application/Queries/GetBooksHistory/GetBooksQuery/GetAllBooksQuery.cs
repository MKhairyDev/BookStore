using System.Collections.Generic;
using BookStore.Application.Dto;
using BookStore.Application.Wrappers;
using MediatR;

namespace BookStore.Application.Queries.GetBooksHistory.GetBooksQuery
{
    public class GetAllBooksQuery :  IRequest<Response<IEnumerable<BookDto>>>
    {
    }
}