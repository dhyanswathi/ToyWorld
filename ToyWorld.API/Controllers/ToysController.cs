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
    [Authorize]
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
        public List<ToyResponse> GetToys()
        {
            return _repo.GetAllToys().ToList();
        }

        [HttpGet("Id")]
        public Toy? GetToyById(Guid Id)
        {
            var toy = _repo.GetToy(Id);

            if (toy == null)
            {
                return new Toy();
            }

            return new Toy()
            {
                Name = toy.Name,
                Description = toy.Description,
                Image = toy.Image
            };
        }

        [HttpPost]
        public IActionResult AddToy(ToyRequest toyDto)
        {
            var result = _repo.CreateToy(toyDto);
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
        public void DeleteStudent(Guid Id)
        {
            _repo.DeleteToy(Id);
        }

        [HttpPut]
        [Route("image/{id}")]
        public async Task<ActionResult> AddCv([FromForm] FileUploadModel file, Guid id)
        {
            if ( _repo.GetToy(id) == null)
            {
                return NotFound();
            }

            try
            {
                 _repo.UploadImage(file, id);

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("File too big, 4Mb limit");
            }

        }
    }
}
