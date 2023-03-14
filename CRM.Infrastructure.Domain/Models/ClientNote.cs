using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Domain
{
    public class ClientNote: BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int ClientId { get; set; }
        public int UserId { get; set; }
    }
}
