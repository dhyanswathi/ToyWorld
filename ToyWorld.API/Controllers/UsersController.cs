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
    }
}
