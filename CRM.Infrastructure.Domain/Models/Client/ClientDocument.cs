using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Domain
{
    public class ClientDocument : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ContentType { get; set; }
        [Required]
        public byte[] Content { get; set; }
        public int ClientId { get; set; }
    }
}
