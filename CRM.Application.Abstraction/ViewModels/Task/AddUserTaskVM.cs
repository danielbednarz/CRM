using CRM.Infrastructure.Dictionaries;

namespace CRM.Application.Abstraction
{
    public class AddUserTaskVM
    {
        public string Description { get; set; }
        public int? AssignedUserId { get; set; }
        public int? SupervisorId { get; set; }
        public int? ClientId { get; set; }
        public DateTime? CompletionDate { get; set; }
        public UserTaskPriorityType Priority { get; set; }
        public bool RequireConfirmation { get; set; }
    }
}
