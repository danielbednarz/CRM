using CRM.Infrastructure.Domain;

namespace CRM.Data.Abstraction
{
    public interface IClientNotesRepository : IGenericRepository<ClientNote>
    {
        Task<List<ClientNote>> GetClientNotes(int clientId);
    }
}
