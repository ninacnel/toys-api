using AutoMapper;
using Data;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;

namespace Repository
{
    public class RoleRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public RoleRepository(DataContext context)
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
            var role = _context.roles.SingleOrDefault(r => r.RoleId == id);
            var roleDTO = _mapper.Map<RoleDTO>(role);
            var response = roleDTO.RoleName;
            return response;
        }

        public RoleDTO AddRole(RoleViewModel role)
        {
            RoleDTO newRole = new RoleDTO();

            _context.roles.Add(new Role
            {
                RoleName = role.RoleName,
            });

            _context.SaveChanges();

            newRole.RoleName = role.RoleName;

            return newRole;
        }

        public RoleDTO ModifyRole(RoleViewModel role)
        {
            Role roleDB = _context.roles.Single(r => r.RoleId == role.RoleId);
            RoleDTO newRole = new RoleDTO();

            roleDB.RoleName = role.RoleName;

            _context.SaveChanges();

            newRole.RoleName = role.RoleName;

            return newRole;
        }
    }
}
