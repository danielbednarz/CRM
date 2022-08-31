using CRM.Data.Abstraction;
using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;

namespace CRM.Data.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(MainDatabaseContext context) : base(context)
        {
        }

    }
}
