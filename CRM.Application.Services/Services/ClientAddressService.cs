using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using CRM.Infrastructure.Domain.Models;
using System.Net.Mail;

namespace CRM.Application.Services
{
    public class ClientAddressService : IClientAddressService
    {
        private readonly IClientRepository _clientRepository;

        public ClientAddressService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task AddPhoneNumber(int clientId, string phoneNumber)
        {
            Client client = GetClient(clientId);

            ClientPhoneNumber clientPhoneNumber = new()
            {
                ClientId = client.Id,
                PhoneNumber = phoneNumber,
            };

            client.ClientPhoneNumbers.Add(clientPhoneNumber);
            await _clientRepository.SaveAsync();
        }

        public bool DeletePhoneNumber(int clientId, Guid phoneNumberId)
        {
            Client client = GetClient(clientId);

            ClientPhoneNumber numberToDelete = client.ClientPhoneNumbers.FirstOrDefault(x => x.Id == phoneNumberId);

            if (numberToDelete == null)
            {
                throw new Exception("Cannot find number to delete");
            }

            if (!client.ClientPhoneNumbers.Remove(numberToDelete))
            {
                return false;
            }

            _clientRepository.Save();
            return true;
        }

        public async Task<bool> AddEmail(int clientId, string emailAddress)
        {
            Client client = GetClient(clientId);

            if (!IsEmailValid(emailAddress))
            {
                return false;
                //throw new Exception("Value is not an email address");
            }

            ClientEmail clientEmail = new()
            {
                ClientId = client.Id,
                Email = emailAddress,
            };

            client.ClientEmails.Add(clientEmail);
            await _clientRepository.SaveAsync();

            return true;
        }

        public bool DeleteEmail(int clientId, Guid emailId)
        {
            Client client = GetClient(clientId);

            ClientEmail emailToDelete = client.ClientEmails.FirstOrDefault(x => x.Id == emailId);

            if (emailToDelete == null)
            {
                throw new Exception("Cannot find email to delete");
            }

            if (!client.ClientEmails.Remove(emailToDelete))
            {
                return false;
            }

            _clientRepository.Save();
            return true;
        }

        private Client GetClient(int clientId)
        {
            Client client = _clientRepository.Find(clientId);

            if (client == null)
            {
                throw new Exception("Cannot find client with given id");
            }

            return client;
        }

        private static bool IsEmailValid(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
    }
}
