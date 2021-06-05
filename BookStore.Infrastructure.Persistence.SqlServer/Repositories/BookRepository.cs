using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Domain.Entities;
using BookStore.Infrastructure.Persistence.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence.SqlServer.Repositories
{
    public class BookRepository: GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookStoreDbContext context):base(context)
        {
          
        }
        public async Task<Book> GetBookWithAuthorAsync(int id)
        {
            return await Context.Books.Include(x => x.Authors).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
