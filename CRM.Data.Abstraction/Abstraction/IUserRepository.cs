using CRM.Infrastructure.Domain;

namespace CRM.Data.Abstraction
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<string> GetUserNameSurnameString(int? id);
        Task<List<AppRole>> GetUserRoles(int id);
        Task<List<AppUser>> GetUsersNotInRole(int roleId);
    }
}
