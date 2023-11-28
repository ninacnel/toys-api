using Data.DTOs;
using Data.ViewModels;
using Repository;
using Services.IServices;

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

        public UserDTO UpdateUser(UserViewModel user)
        {
            return _repository.UpdateUser(user);
        }

        public void DeleteUser(int id)
        {
            _repository.DeleteUser(id);
        }

        public void SoftDeleteUser(int id)
        {
            _repository.SoftDeleteUser(id);
        }

        public void RecoverUser(int id)
        {
            _repository.RecoverUser(id);
        }
    }
}
