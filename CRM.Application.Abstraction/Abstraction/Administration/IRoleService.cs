namespace CRM.Application.Abstraction
{
    public interface IRoleService
    {
        Task<List<RoleDTO>> GetRoles();
        Task<RoleDetailsDTO> GetRoleDetails(int id);
        Task DeleteUserFromRole(int userId, int roleId);
        Task<List<UserSelectDTO>> GetUsersAvailableToAdd(int roleId);
        Task AddUsersToRole(int roleId, List<int> userIds);
    }
}
