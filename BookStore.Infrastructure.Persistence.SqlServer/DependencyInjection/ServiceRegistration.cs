using BookStore.Application.Interfaces.Repositories;
using BookStore.Infrastructure.Persistence.SqlServer.Contexts;
using BookStore.Infrastructure.Persistence.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.Persistence.SqlServer.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

                services.AddDbContext<BookStoreDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("BookStoreConnectionString")));

                services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient(typeof(IGenericCrudRepository<>), typeof(GenericRepository<>));

        }
    }
}
