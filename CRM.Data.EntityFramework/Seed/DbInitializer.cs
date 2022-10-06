using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;

namespace CRM.EntityFramework.Seed
{
    public class DbInitializer
    {
        public static void Seed(MainDatabaseContext context, UserManager<AppUser> userManager)
        {
            AddUsers(context, userManager);
        }

        private static void AddUsers(MainDatabaseContext context, UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any())
            {
                return;
            }

            List<AppUser> users = new()
            {
                new AppUser
                {
                    UserName = "admin@admin.pl",
                    Email = "admin@admin.pl",
                    FirstName = "Jan",
                    LastName = "Kowalski",
                }
            };

            foreach(var user in users)
            {
                userManager.CreateAsync(user, "Start.123");
            }

            context.SaveChanges();
        }
    }
}
