using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Services.Converters
{
    public class UserConverter
    {
        public static AppUserVM ToAppUserVM(AppUser model)
        {
            return new AppUserVM
            {
                Id = model.Id,
                FullName = model.FirstName + " " + model.LastName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            };
        }
    }
}
