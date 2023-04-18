using CRM.Application.Abstraction;

namespace CRM.Application.Abstraction
{
    public interface IAccountService
    {
        public Task Register(RegisterVM model);
        public Task ConfirmEmail(int id, string token);
        public Task<UserDTO> Login(LoginVM model);
    }
}
