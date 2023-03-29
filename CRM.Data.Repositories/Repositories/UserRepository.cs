using CRM.Data.Abstraction;
using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(MainDatabaseContext context) : base(context)
        {
        }

        public async Task<string> GetUserNameSurnameString(int? id)
        {
            AppUser user = await _context.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return "";
            }

            return $"{user.FirstName} {user.LastName}";
        }
    }
}
