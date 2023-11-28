using Data.DTOs;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
