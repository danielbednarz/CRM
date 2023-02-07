using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using ExcelDataReader;

namespace CRM.Application.Services
{
    public class ClientImportService : IClientImportService
    {
        private readonly IClientRepository _clientRepository;

        public ClientImportService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int> ImportClientsFromXlsxFile(byte[] fileContent)
        {
            using var stream = new MemoryStream(fileContent);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            int addedClientsCount = 0;

            while (reader.Read())
            {
                if (reader.GetString(0) == "Nazwa")
                {
                    reader.Read();
                }

                string nip = reader.GetString(1);

                if (!await _clientRepository.CheckIfClientExistsByNip(nip))
                {
                    Client client = new()
                    {
                        Name = reader.GetString(0),
                        Nip = nip,
                        Krs = reader.GetString(2),
                        Regon = reader.GetString(3),
                        Country = reader.GetString(4),
                        City = reader.GetString(5),
                        Street = reader.GetString(6),
                        BuildingNumber = reader.GetString(7),
                        IsActive = reader.GetString(8) == "Tak"
                    };

                    _clientRepository.Add(client);
                    if (await _clientRepository.SaveAsync() <= 0)
                    {
                        throw new Exception("Error when trying to add client");
                    }
                    addedClientsCount++;
                }

            }

            return addedClientsCount;
        }
    }
}
