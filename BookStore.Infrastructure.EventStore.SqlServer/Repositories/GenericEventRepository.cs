using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Application.Interfaces.Repositories;
using BookStore.Infrastructure.EventStore.SqlServer.Contexts;

namespace BookStore.Infrastructure.EventStore.SqlServer.Repositories
{
    public class GenericEventRepository<TEntity> : IGenericEventRepository<TEntity> where TEntity : class
    {
        protected readonly LoggedEventDbContext Context;

        public GenericEventRepository(LoggedEventDbContext context)
        {
            Context = context;
        }

        public async Task AppendAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity: entity);
        }

        public async Task AppendRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities: entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync() > 0;
        }
    }
}