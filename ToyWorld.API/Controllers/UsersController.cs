using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToyWorld.API.DTO;
using ToyWorld.API.Models;
using ToyWorld.API.Services;

namespace ToyWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly ITokenManager _tokenManager;
        public UsersController(IUserRepository repo, ITokenManager tokenManager) 
        { 
            _repo = repo;
            _tokenManager = tokenManager;   
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var result = await _repo.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> GetUser(Guid id)
        {
            var user = await _repo.GetUserById(id);

            if (user == null)
            {
                return NotFound("A user with this id do not exist");
            }

            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<ActionResult> PostUser(UserRegister user)
        {
            var result = await _repo.Register(user);
            return Created("", result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser(UserLogin userLogin)
        {
            var result = await _repo.GetAllUsers();
            var user = result.FirstOrDefault(x => x.Email == userLogin.Email);

            if (user != null && user.Password == userLogin.Password)
            {
                var token = _tokenManager.Authenticate(userLogin.Email, userLogin.Password);
                if (token == null)
                {
                    return Unauthorized();
                }

                var loginUser = new LoginResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Token = token
                };
                return Ok(loginUser);
            }

            return Unauthorized("Email or password is incorrect");
        }
    }
}
