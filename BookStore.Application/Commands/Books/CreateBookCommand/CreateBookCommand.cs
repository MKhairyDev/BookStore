using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Commands.Books.CreateBookCommand
{
   public class CreateBookCommand:Book,IRequest<Response<Book>>
    {
    }
}
