using CRM.Infrastructure.Domain;

namespace CRM.Application.Abstraction
{
    public interface ITokenService
    {
        public Task<string> CreateToken(AppUser user);
    }
}
