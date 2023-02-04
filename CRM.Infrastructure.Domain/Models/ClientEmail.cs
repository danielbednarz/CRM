using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Domain.Models
{
    public class ClientEmail
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int ClientId { get; set; }
    }
}
