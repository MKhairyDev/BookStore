using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Interfaces.Repositories
{
   public interface IRepository<TEntity>where TEntity:class
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entities);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
