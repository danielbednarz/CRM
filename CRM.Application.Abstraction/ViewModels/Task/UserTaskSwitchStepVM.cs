namespace CRM.Application.Abstraction
{
    public class UserTaskSwitchStepVM
    {
        public Guid TaskId { get; set; }
        public bool RequireConfirmation { get; set; }
    }
}
