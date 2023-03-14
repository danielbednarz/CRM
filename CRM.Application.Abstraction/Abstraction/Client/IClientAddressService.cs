namespace CRM.Application.Abstraction
{
    public interface IClientAddressService
    {
        Task AddPhoneNumber(int clientId, string phoneNumber);
        bool DeletePhoneNumber(int clientId, Guid phoneNumberId);
        Task<bool> AddEmail(int clientId, string emailAddress);
        bool DeleteEmail(int clientId, Guid emailId);
    }
}
