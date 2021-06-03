using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Application.Commands.Interfaces.Repositories
{
    public interface IEventRepository<TEntity> where TEntity : class
    {
        Task StoreAsync(TEntity entity);
    }
}
