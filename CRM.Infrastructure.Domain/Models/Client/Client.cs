using CRM.Infrastructure.Domain.Models;

namespace CRM.Infrastructure.Domain
{
    public class Client : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Krs { get; set; }
        public string Regon { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public bool IsActive { get; set; } = true;
        public int Rating { get; set; } = 5;
        public virtual ICollection<ClientPhoneNumber> ClientPhoneNumbers { get; set; }
        public virtual ICollection<ClientEmail> ClientEmails { get; set; }
    }
}
