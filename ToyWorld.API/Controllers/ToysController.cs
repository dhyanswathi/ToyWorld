using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using ToyWorld.API.Models;
using ToyWorld.API.Services;

namespace ToyWorld.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public List<DTO.Toy> GetToys()
        {
            return _repo.GetAll()
                .Select(x => new DTO.Toy()
                {
                    Name = x.Name,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl
                }).ToList();
        }

        [HttpGet("Id")]
        public DTO.Toy? GetToyById(int Id)
        {
            var toy = _repo.GetById(Id);

            if (toy == null)
            {
                return new DTO.Toy();
            }

            return new DTO.Toy()
            {
                Name = toy.Name,
                Description = toy.Description,
                ImageUrl = toy.ImageUrl
            };
        }

        [HttpPost]
        public IActionResult AddToy(DTO.Toy toyDto)
        {
            var toy = new Toy()
            {
                Name = toyDto.Name,
                Description = toyDto.Description,
                ImageUrl = toyDto.ImageUrl
            };

            var id = _repo.Add(toy);
            return CreatedAtAction(nameof(GetToyById), new { id = id }, toyDto);
        }

        [HttpPut]
        public void UpdateToy(DTO.Toy toyDto, int id)
        {
            var toy = _repo.GetById(id);
            if (toy != null)
            {
                toy.Name = toyDto.Name;
                toy.Description = toyDto.Description;
                toy.ImageUrl = toyDto.ImageUrl;
                
                _repo.Update(toy);
            }
        }

        [HttpPost]
        [Route("sendEmail")]
        public async Task<ActionResult> SendEmailTo(MailRequest request)
        {
            await _email.SendEmail(request);
            return Ok();
        }

        [HttpDelete]
        public void DeleteStudent(int Id)
        {
            _repo.Delete(Id);
        }
    }
}
