using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;

namespace Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _repository;

        public RoleService(RoleRepository repository)
        {
            _repository = repository;
        }

        public List<RoleDTO> GetRoles()
        {
            return _repository.GetRoles();
        }

        public string GetRoleById(int id)
        {
            return _repository.GetRoleById(id);
        }

        public RoleDTO AddRole(RoleViewModel role)
        {
            return _repository.AddRole(role);
        }
        public RoleDTO ModifyRole(RoleViewModel role)
        {
            return _repository.ModifyRole(role);
        }
    }
}
