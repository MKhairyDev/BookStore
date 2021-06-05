using System.Collections.Generic;
using BookStore.Application.Dto;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Commands.Books.UpdateBookCommand
{
   public class UpdateBookCommand: BookDto,IRequest<Response<Book>>
    {
    }
}
