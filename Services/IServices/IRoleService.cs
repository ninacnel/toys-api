using Data.DTOs;
using Data.ViewModels;

namespace Services.IServices
{
    public interface IRoleService
    {
        List<RoleDTO> GetRoles();
        string GetRoleById(int id);
        RoleDTO AddRole(RoleViewModel role);
        RoleDTO ModifyRole(RoleViewModel role);
    }
}
