namespace CRM.Application.Abstraction
{
    public class AddUsersToRoleVM
    {
        public int RoleId { get; set; }
        public List<int> UserIds { get; set; }
    }
}
