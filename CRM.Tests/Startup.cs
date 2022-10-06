using CRM.EntityFramework.Context;
using CRM.Infrastructure.DependencyResolver;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CRM.Tests
{
    public class Startup
    {
        static IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        public void ConfigureServices(IServiceCollection services) 
        {
            

        services.AddDbContext<MainDatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:MainDatabaseContext"].ToString());
            });

            services.AddSingleton(Configuration);
            services.AddOptions();
            services.AddCors();

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"].ToString())),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            CoreBindings.Load(services);

        }


    }
}
