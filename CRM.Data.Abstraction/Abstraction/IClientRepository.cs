using CRM.Infrastructure.Domain;

namespace CRM.Data.Abstraction
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<bool> CheckIfClientExistsByNip(string nip);
    }
}
