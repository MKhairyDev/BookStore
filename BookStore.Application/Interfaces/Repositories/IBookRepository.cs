using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces.Repositories
{
   public interface IBookRepository:IGenericCrudRepository<Book>
    {
        Task<Book> GetBookWithAuthorAsync(int id);
    }
}
