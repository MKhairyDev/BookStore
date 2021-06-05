using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.Parameters;

namespace BookStore.Application.Interfaces.Repositories
{
    public interface IGenericEventRepository<TEntity> where TEntity : class
    {
        Task AppendAsync(TEntity entity);
        Task AppendRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> SaveChangesAsync();
    }
}
