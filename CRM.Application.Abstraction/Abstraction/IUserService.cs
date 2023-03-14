using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Abstraction
{
    public interface IUserService
    {
        Task<List<AppUserVM>> GetUsersAsync();
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetAdminAsync();
    }
}
