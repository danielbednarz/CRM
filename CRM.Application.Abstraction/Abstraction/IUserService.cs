using CRM.Application.Abstraction.ViewModels;

namespace CRM.Application.Abstraction
{
    public interface IUserService
    {
        Task<List<AppUserVM>> GetUsers();
    }
}
