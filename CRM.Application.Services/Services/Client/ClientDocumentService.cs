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
            document.ContentType = GetMimeType(document.Name);

            _clientDocumentRepository.Add(document);
            await _clientDocumentRepository.SaveAsync();
        }

        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();

            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

            if (regKey != null && regKey.GetValue("Content Type") != null)
            {
                mimeType = regKey.GetValue("Content Type").ToString();
            }

            return mimeType;
        }
    }
}
