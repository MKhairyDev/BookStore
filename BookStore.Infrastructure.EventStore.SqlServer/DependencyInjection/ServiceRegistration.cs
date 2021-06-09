using BookStore.Application.Interfaces.Repositories;
using BookStore.Infrastructure.EventStore.SqlServer.Contexts;
using BookStore.Infrastructure.EventStore.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infrastructure.EventStore.SqlServer.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static void AddEventStoreSqlServerInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<LoggedEventDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("BookStoreConnectionString")));

            services.AddTransient<IBookEventRepository, BookEventRepository>();
            services.AddTransient(typeof(IGenericEventRepository<>), typeof(GenericEventRepository<>));

        }
    }
}