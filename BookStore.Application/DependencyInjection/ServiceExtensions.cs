using System.Reflection;
using BookStore.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Application.DependencyInjection
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());


        }
    }
}