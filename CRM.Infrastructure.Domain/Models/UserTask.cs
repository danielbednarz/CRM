using CRM.Infrastructure.Dictionaries;
using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Domain
{
    public class UserTask: BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Signature { get; set; }
        public string Description { get; set; }
        public int? AssignedUserId { get; set; }
        public virtual AppUser AssignedUser { get; set; }
        public int? SupervisorId { get; set; }
        public virtual AppUser Supervisor { get; set; }
        public bool RequireConfirmation { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }
        public UserTaskStepType Step { get; set; }
        public UserTaskPriorityType Priority { get; set; }
        public int CreatedById { get; set; }
        public virtual AppUser CreatedBy { get; set; }
    }
}
