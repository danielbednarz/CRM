using CRM.Infrastructure.Dictionaries;
using System.ComponentModel.DataAnnotations;

namespace CRM.Infrastructure.Domain
{
    public class UserTaskHistory : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserTaskId { get; set; }
        public UserTaskStepType? FromStep { get; set; }
        public UserTaskStepType ToStep { get; set; }
    }
}
