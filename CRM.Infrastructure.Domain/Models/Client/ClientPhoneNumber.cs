using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Domain.Models
{
    public class ClientPhoneNumber
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int ClientId { get; set; }

    }
}
