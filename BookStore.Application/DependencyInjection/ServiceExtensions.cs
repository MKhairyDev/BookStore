using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}