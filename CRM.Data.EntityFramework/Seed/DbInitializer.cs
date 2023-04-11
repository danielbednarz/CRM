using CRM.EntityFramework.Context;
using CRM.Infrastructure.Dictionaries;
using CRM.Infrastructure.Domain;
using CRM.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM.EntityFramework.Seed
{
    public class DbInitializer
    {
        public static async Task Seed(MainDatabaseContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            await AddRoles(roleManager);
            await AddUsers(context, userManager);
            AddClients(context);
        }

        private static async Task AddRoles(RoleManager<AppRole> roleManager)
        {
            if (await roleManager.Roles.AnyAsync())
            {
                return;
            }

            var roles = new List<AppRole>
            {
                new AppRole { Name = AppRoleType.Admin },
                new AppRole { Name = AppRoleType.Supervisor },
                new AppRole { Name = AppRoleType.User },
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }

        private static async Task AddUsers(MainDatabaseContext context, UserManager<AppUser> userManager)
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
                },
                new AppUser
                {
                    UserName = "daniel@crm.pl",
                    Email = "daniel@crm.pl",
                    FirstName = "Daniel",
                    LastName = "Bednarz",
                },
                new AppUser
                {
                    UserName = "adam@crm.pl",
                    Email = "adam@crm.pl",
                    FirstName = "Adam",
                    LastName = "Testowy",
                }
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Start.123");
                await userManager.AddToRoleAsync(user, AppRoleType.User);
                if (user.UserName == "daniel@crm.pl" || user.UserName == "admin@admin.pl")
                {
                    await userManager.AddToRoleAsync(user, AppRoleType.Supervisor);
                }
                if (user.UserName == "admin@admin.pl")
                {
                    await userManager.AddToRoleAsync(user, AppRoleType.Admin);
                }
            }

            context.SaveChanges();
        }

        private static void AddClients(MainDatabaseContext context)
        {
            if (!context.Clients.Any())
            {
                List<Client> clients = new()
                {
                    new Client
                    {
                        Name = "Firma Testowa",
                        Nip = "567890345",
                        Street = "Chęcińska",
                        City = "Kielce",
                        Country = "Polska",
                        BuildingNumber = "10",
                        Rating = 5,
                        ClientEmails = new List<ClientEmail>
                        {
                            new ClientEmail
                            {
                                Email = "testowa@test.pl"
                            },
                            new ClientEmail
                            {
                                Email = "testowa2@test.pl"
                            }
                        },
                        ClientPhoneNumbers = new List<ClientPhoneNumber>
                        {
                            new ClientPhoneNumber
                            {
                                PhoneNumber = "600454241"
                            },
                            new ClientPhoneNumber
                            {
                                PhoneNumber = "780411687"
                            }
                        }
                    }
                };

                context.Clients.AddRange(clients);
                context.SaveChanges();
            }


        }
    }
}
