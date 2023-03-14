namespace CRM.Application.Abstraction
{
    public interface INipService
    {
        Task<ClientDataDTO> GetClientDataByNip(string nip);
    }
}
