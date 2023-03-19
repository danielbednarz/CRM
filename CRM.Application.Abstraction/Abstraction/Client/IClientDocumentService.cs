using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Abstraction
{
    public interface IClientDocumentService
    {
        Task Add(ClientDocument document);
    }
}
