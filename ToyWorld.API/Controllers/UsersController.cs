using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToyWorld.API.DTO;
using ToyWorld.API.Models;

namespace ToyWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        public UsersController(IUserRepository repo) 
        { 
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _repo.GetAllUsers().ToList();
            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult PostUser(UserRegister user)
        {
            var result = _repo.Register(user);
            return Created("", result);
        }
    }
}
