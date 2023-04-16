namespace CRM.Application.Abstraction
{
    public class UserDetailsDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public List<RoleDTO> Roles { get; set; }
    }
}
