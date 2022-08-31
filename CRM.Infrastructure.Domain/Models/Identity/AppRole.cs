using Microsoft.AspNetCore.Identity;

namespace CRM.Infrastructure.Domain
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
