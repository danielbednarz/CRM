using Microsoft.AspNetCore.Identity;

namespace CRM.Infrastructure.Domain
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
