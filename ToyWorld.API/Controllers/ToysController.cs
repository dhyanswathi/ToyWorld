using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using ToyWorld.API.DTO;
using ToyWorld.API.Models;
using ToyWorld.API.Services;

namespace ToyWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ToysController : ControllerBase
    {
        private IToyRepository _repo;
        private Services.IEmailService _email;
        public ToysController(IToyRepository repo, Services.IEmailService email)
        {
            _repo = repo;
            _email = email;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToyResponse>>> GetToys()
        {
            var toys = await _repo.GetAllToys();
            var toyResponses = toys.Select(x => new ToyResponse()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Image = x.Image
            });

            return Ok(toyResponses);
        }

        [HttpGet("Id")]
        public async Task<ActionResult<Toy?>> GetToyById(Guid Id)
        {
            var toy = await _repo.GetToy(Id);

            if (toy == null)
            {
                return NotFound("A toy with this id do not exist");
            }

            return Ok(toy);
        }

        [HttpPost]
        public async Task<ActionResult> AddToy(ToyRequest toyDto)
        {
            var result = await _repo.CreateToy(toyDto);
            return Created("", result);
        }


        [HttpPost]
        [Route("sendEmail")]
        public async Task<ActionResult> SendEmailTo(MailRequest request)
        {
            await _email.SendEmail(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteStudent(Guid Id)
        {
            await _repo.DeleteToy(Id);
            return NoContent();
        }

        [HttpPut]
        [Route("image/{id}")]
        public async Task<ActionResult> AddImage([FromForm] FileUploadModel file, Guid id)
        {
            if ( _repo.GetToy(id) == null)
            {
                return BadRequest("A toy with this id does not exist");
            }

            await _repo.UploadImage(file, id);

            return NoContent();
        }
    }
}
