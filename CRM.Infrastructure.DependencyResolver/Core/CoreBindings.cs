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
            services.AddScoped<IClientNotesService, ClientNotesService>();
            services.AddScoped<IClientDocumentService, ClientDocumentService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<ITaskCommentService, TaskCommentService>();
            services.AddScoped<ITaskHistoryService, TaskHistoryService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();

            services.TryAddScoped<UserManager<AppUser>>();
            services.TryAddScoped<SignInManager<AppUser>>();
            services.TryAddScoped<RoleManager<AppRole>>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientNotesRepository, ClientNotesRepository>();
            services.AddScoped<IClientDocumentRepository, ClientDocumentRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
            services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();

            return services;
        }
    }
}
