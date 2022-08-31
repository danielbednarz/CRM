using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;

namespace CRM.WebAPI
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<AppRole>>()
                .AddEntityFrameworkStores<MainDatabaseContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddServiceDependencies(this IServiceCollection services)
        {

        }
    }
}
