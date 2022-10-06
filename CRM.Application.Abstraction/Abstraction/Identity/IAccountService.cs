using CRM.Application.Abstraction;

namespace CRM.Application.Abstraction
{
    public interface IAccountService
    {
        public Task<UserDTO> Login(LoginVM model);
    }
}
