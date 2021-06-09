using System.Collections.Generic;
using BookStore.Application.Dto;
using BookStore.Application.Wrappers;
using BookStore.Domain.Entities;
using MediatR;

namespace BookStore.Application.Commands.Books.CreateBookCommand
{
   public class CreateBookCommand: BookDto,IRequest<Response<BookDto>>
    {
    }
}
