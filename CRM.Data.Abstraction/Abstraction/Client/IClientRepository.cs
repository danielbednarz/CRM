using CRM.Infrastructure.Domain;

namespace CRM.Data.Abstraction
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<Client> GetClientById(int id);
        Task<bool> CheckIfClientExistsByNip(string nip);
    }
}
