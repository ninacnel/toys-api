using Data.DTOs;
using Data.ViewModels;

namespace Services.IServices
{
    public interface IUserService
    {
        List<UserDTO> GetUsers();
        UserDTO GetUserById(int id);
        UserDTO AddUser(UserViewModel user);
        UserDTO UpdateUser(UserViewModel user);
        void DeleteUser(int id);
        void SoftDeleteUser(int id);
        void RecoverUser(int id);
    }
}
