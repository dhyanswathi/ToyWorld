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
        public IActionResult GetUsers()
        {
            var result = _repo.GetAllUsers().ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(Guid id)
        {
            try
            {
                var user = _repo.GetUserById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.ToString());
            }
        }

        [HttpPost("register")]
        public IActionResult PostUser(UserRegister user)
        {
            var result = _repo.Register(user);
            return Created("", result);
        }

        [HttpPost("login")]
        public IActionResult LoginUser(UserLogin userLogin)
        {
            var result = _repo.GetAllUsers();
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
