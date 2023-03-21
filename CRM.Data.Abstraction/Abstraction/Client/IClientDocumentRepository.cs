using CRM.Infrastructure.Domain;

namespace CRM.Data.Abstraction
{
    public interface IClientDocumentRepository : IGenericRepository<ClientDocument>
    {
        Task<List<ClientDocument>> GetClientDocuments(int clientId);
    }
}
