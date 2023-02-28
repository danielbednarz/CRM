using CRM.Application.Abstraction;
using CRM.Application.Services;
using CRM.Data.Abstraction;
using CRM.Data.Repositories;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CRM.Infrastructure.DependencyResolver
{
    public static class CoreBindings
    {
        public static IServiceCollection Load(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<INipService, NipService>();
            services.AddScoped<IClientImportService, ClientImportService>();
            services.AddScoped<IClientAddressService, ClientAddressService>();

            services.TryAddScoped<UserManager<AppUser>>();
            services.TryAddScoped<SignInManager<AppUser>>();
            services.TryAddScoped<RoleManager<AppRole>>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
