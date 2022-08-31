using CRM.Application.Abstraction;
using CRM.Application.Services;
using CRM.Data.Abstraction;
using CRM.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Infrastructure.DependencyResolver
{
    public static class CoreBindings
    {
        public static IServiceCollection Load(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}
