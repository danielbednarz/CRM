using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Domain.Models
{
    public class ClientPhoneNumbers
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public virtual Client Client { get; set; }

    }
}
