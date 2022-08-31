using Microsoft.AspNetCore.Identity;

namespace CRM.Infrastructure.Domain
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
