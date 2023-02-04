using CRM.Infrastructure.Domain.Models;

namespace CRM.Infrastructure.Domain
{
    public class Client : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Krs { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public decimal Rating { get; set; }
        public ICollection<ClientPhoneNumbers> ClientPhoneNumbers { get; set; }
        public ICollection<ClientEmails> ClientEmails { get; set; }
    }
}
