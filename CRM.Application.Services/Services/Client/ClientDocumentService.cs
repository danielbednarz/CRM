using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Services
{
    public class ClientDocumentService : IClientDocumentService
    {
        private readonly IClientDocumentRepository _clientDocumentRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public ClientDocumentService(IClientDocumentRepository clientDocumentRepository, IClientRepository clientRepository, IUserRepository userRepository)
        {
            _clientDocumentRepository = clientDocumentRepository;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public async Task Add(ClientDocument document)
        {
            _clientDocumentRepository.Add(document);
            await _clientDocumentRepository.SaveAsync();
        }

        public async Task<List<ClientDocumentDTO>> GetClientDocuments(int clientId)
        {
            List<ClientDocument> clientDocuments = await _clientDocumentRepository.GetClientDocuments(clientId);
            List<ClientDocumentDTO> documents = new();

            foreach (var document in clientDocuments)
            {
                documents.Add(new ClientDocumentDTO
                {
                    Id = document.Id,
                    Name = document.Name,
                    ContentType = document.ContentType
                });
            }

            return documents;
        }

        public void DeleteDocument(Guid documentId)
        {
            ClientDocument documentToDelete = _clientDocumentRepository.FirstOrDefault(x => x.Id == documentId);

            _clientDocumentRepository.Remove(documentToDelete);
            _clientDocumentRepository.Save();
        }
    }
}
