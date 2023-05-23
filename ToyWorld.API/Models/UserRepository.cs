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
        public async Task DeleteUser(Guid id)
        {
            var user = await GetUserById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await SaveUser();
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> Register(UserRegister registerUser)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = registerUser.Name,
                Email = registerUser.Email,
                Password = registerUser.Password
            };

            await _context.Users.AddAsync(user);
            await SaveUser();

            return user;
        }

        public async Task SaveUser()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(Guid id, UserRegister updateUser)
        {
            var user = await GetUserById(id);

            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.Password = updateUser.Password;

            _context.Users.Update(user);
            await SaveUser();
        }
    }
}
