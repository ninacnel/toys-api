using AutoMapper;
using Data.DTOs;
using Data.Mappings;
using Data.Models;
using Data.ViewModels;
using BCrypt.Net;
using Data;

namespace Repository
{
    public class UserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly EmailRepository _email;

        public UserRepository(DataContext context, EmailRepository emailRepository)
        {
            _context = context;
            _mapper = AutoMapperConfig.Configure();
            _email = emailRepository;
        }

        public List<UserDTO> GetUsers()
        {
            var users = _context.users.ToList();
            var response = _mapper.Map<List<UserDTO>>(users);
            return response;
        }

        public UserDTO GetUserById(int id)
        {
            var user = _context.users.SingleOrDefault(u => u.UserId == id);
            var response = _mapper.Map<UserDTO>(user);
            return response;
        }

        public UserDTO AddUser(UserViewModel user)
        {
            UserDTO newUser = new UserDTO();

            // Hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Save the user to the database with the hashed password
            _context.users.Add(new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = hashedPassword, // Save the hashed password
                RoleId = user.RoleId,
                State = true,
            });

            _context.SaveChanges();

            newUser.Name = user.Name;
            newUser.Email = user.Email;
            newUser.Password = hashedPassword; // Save the hashed password
            newUser.RoleId = user.RoleId;
            newUser.State = true;

            var emailDto = new EmailDto
            {
                To = user.Email,
                Subject = "Account Created",
                Body = "Your account has been successfully created."
            };

            _email.SendEmail(emailDto);

            return newUser;
        }

        public UserDTO UpdateUser(UserViewModel user)
        {
            User userDB = _context.users.Single(f => f.UserId == user.UserId);
            UserDTO newUser = new UserDTO();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            userDB.Name = user.Name;
            userDB.Email = user.Email;
            userDB.Password = hashedPassword;
            userDB.RoleId = user.RoleId;
            
            _context.SaveChanges();

            newUser.Name = user.Name;
            newUser.Email = user.Email;
            newUser.Password = hashedPassword;
            newUser.RoleId = user.RoleId;
            
            return newUser;
        }

        public void DeleteUser(int id)
        {
            _context.users.Remove(_context.users.Single(u => u.UserId == id));
            _context.SaveChanges();
        }
        public void SoftDeleteUser(int id)
        {
            User user = _context.users.Single(u => u.UserId == id);
            if (user.State == true)
            {
                user.State = false;
            }
            _context.SaveChanges();
        }

        public void RecoverUser(int id)
        {
            User user = _context.users.Single(u => u.UserId == id);
            if(user.State == false)
            {
                user.State = true;
            }
            _context.SaveChanges();
        }

    }
}
