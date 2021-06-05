using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces.Repositories
{
   public interface IGenericCrudRepository<TEntity>where TEntity:class
    {
        Task<TEntity> GetAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entities);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<bool> SaveChangesAsync();
    }
}
