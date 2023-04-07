using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Domain
{
    public class UserTaskComment : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public int CreatedById { get; set; }
        public virtual AppUser CreatedBy { get; set; }
        public Guid UserTaskId { get; set; }
        public virtual UserTask UserTask { get; set; }
    }
}
