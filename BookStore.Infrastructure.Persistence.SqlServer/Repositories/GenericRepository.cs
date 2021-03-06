using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Infrastructure.Persistence.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Persistence.SqlServer.Repositories
{
   public class GenericRepository<TEntity> : IGenericCrudRepository<TEntity> where TEntity : class
    {
        protected readonly BookStoreDbContext Context;
        public GenericRepository(BookStoreDbContext context)
        {
            Context = context;
        }
        public async Task<TEntity> GetAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public async Task AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }
        public void Update(TEntity entity)
        {
             Context.Set<TEntity>().Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
             Context.Set<TEntity>().UpdateRange(entities);
        }
        public  void Remove(TEntity entities)
        {
             Context.Set<TEntity>().Remove(entities);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
             Context.Set<TEntity>().RemoveRange(entities);
        }
        public async Task<bool> SaveChangesAsync()
        {
          return  await Context.SaveChangesAsync()>0;
        }
    }
}
