using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Abstraction
{
    public interface IUserService
    {
        Task<List<UserDetailsDTO>> GetUsersAsync();
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<UserDetailsDTO> GetUserDetails(int id);
        Task<AppUser> GetAdminAsync();
        Task EditUser(EditUserVM model);
        Task<List<UserSelectDTO>> GetUsersToSelect();
    }
}
