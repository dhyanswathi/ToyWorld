using ToyWorld.API.DTO;

namespace ToyWorld.API.Models
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(Guid id);
        Task<List<User>> GetAllUsers();
        Task<User> Register(UserRegister registerUser);
        Task DeleteUser(Guid id);
        Task UpdateUser(Guid id, UserRegister updateUser);
        Task SaveUser();
    }
}
