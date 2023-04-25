namespace CRM.Application.Abstraction
{
    public interface IRoleService
    {
        public Task<List<RoleDTO>> GetRoles();
    }
}
