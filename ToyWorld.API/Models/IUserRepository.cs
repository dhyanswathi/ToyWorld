using ToyWorld.API.DTO;

namespace ToyWorld.API.Models
{
    public interface IUserRepository
    {
        User? GetUserById(Guid id);
        IEnumerable<User> GetAllUsers();
        User Register(UserRegister registerUser);
        void DeleteUser(Guid id);
        void UpdateUser(Guid id, UserRegister updateUser);
        void SaveUser();
    }
}
