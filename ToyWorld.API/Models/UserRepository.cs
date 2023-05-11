using Microsoft.EntityFrameworkCore;
using ToyWorld.API.DTO;

namespace ToyWorld.API.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly ToysDBContext _context;
        public UserRepository(ToysDBContext context)
        {
            _context = context;
        }
        public void DeleteUser(Guid id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                SaveUser();
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User? GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User Register(UserRegister registerUser)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = registerUser.Name,
                Email = registerUser.Email,
                Password = registerUser.Password
            };

            _context.Users.Add(user);
            SaveUser();

            return user;
        }

        public void SaveUser()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(Guid id, UserRegister updateUser)
        {
            var user = GetUserById(id);

            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.Password = updateUser.Password;

            _context.Users.Update(user);
            SaveUser();
        }
    }
}
