namespace CRM.Application.Abstraction
{
    public interface IClientImportService
    {
        public Task<int> ImportClientsFromXlsxFile(byte[] fileContent);
    }
}
