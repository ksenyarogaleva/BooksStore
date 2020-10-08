using BooksStore.BLL.Interfaces;
using BooksStore.BLL.Services;
using BooksStore.DAL.Interfaces;
using BooksStore.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BooksStore.ServiceExtensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IBooksRepository, BooksRepository>();
            services.AddScoped<IAuthorsRepository, AuthorsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IBooksService, BooksService>();
            services.AddTransient<IAuthorsService, AuthorsService>();
        }
    }
}
