using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Services.Converters
{
    public class UserConverter
    {
        public static AppUserVM ToAppUserVM(AppUser model, IList<string> roles)
        {
            List<string> translatedRoles = new();

            foreach (var role in roles) 
            {
                var translation = role switch
                {
                    "Admin" => "Administratorzy",
                    "Supervisor" => "Przełożeni",
                    "PrivilegedUser" => "Merytorycy",
                    "User" => "Użytkownicy",
                    _ => throw new Exception("Cannot translate role")
                };
                translatedRoles.Add(translation);
            }

            string translatedRolesString = string.Join(", ", translatedRoles);

            return new AppUserVM
            {
                Id = model.Id,
                FullName = model.FirstName + " " + model.LastName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Roles = translatedRolesString
            };
        }
    }
}
