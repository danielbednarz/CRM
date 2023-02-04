using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using CRM.Infrastructure.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace CRM.EntityFramework.Seed
{
    public class DbInitializer
    {
        public static void Seed(MainDatabaseContext context, UserManager<AppUser> userManager)
        {
            AddUsers(context, userManager);
            AddClients(context);
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
