using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public List<UserDTO> GetUsers()
        {
            return _repository.GetUsers();
        }

        public UserDTO GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }

        public UserDTO AddUser(UserViewModel user)
        {
            return _repository.AddUser(user);
        }

        public void DeleteUser(int id)
        {
            _repository.DeleteUser(id);
        }

        public void RecoverUser(int id)
        {
            _repository.RecoverUser(id);
        }
    }
}
