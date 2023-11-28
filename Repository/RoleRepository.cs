using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;

namespace Repository
{
    public class RoleRepository
    {
        private readonly toystoreContext _context;
        private readonly IMapper _mapper;

        public RoleRepository(toystoreContext context)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<RoleDTO> GetRoles()
        {
            var roles = _context.roles.ToList();
            var response = _mapper.Map<List<RoleDTO>>(roles);
            return response;
        }
        
        public string GetRoleById(int id)
        {
            var role = _context.roles.SingleOrDefault(r => r.role_id == id);
            var roleDTO = _mapper.Map<RoleDTO>(role);
            var response = roleDTO.role_name;
            return response;
        }

        public RoleDTO AddRole(RoleViewModel role)
        {
            RoleDTO newRole = new RoleDTO();

            _context.roles.Add(new roles
            {
                role_name = role.role_name,
            });

            _context.SaveChanges();

            newRole.role_name = role.role_name;

            return newRole;
        }

        public RoleDTO ModifyRole(RoleViewModel role)
        {
            roles roleDB = _context.roles.Single(r => r.role_id == role.role_id);
            RoleDTO newRole = new RoleDTO();

            roleDB.role_name = role.role_name;

            _context.SaveChanges();

            newRole.role_name = role.role_name;

            return newRole;
        }
    }
}
