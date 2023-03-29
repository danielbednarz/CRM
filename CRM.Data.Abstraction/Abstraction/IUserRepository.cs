using CRM.Infrastructure.Domain;

namespace CRM.Data.Abstraction
{
    public interface IUserRepository : IGenericRepository<AppUser>
    {
        Task<string> GetUserNameSurnameString(int? id);
    }
}
