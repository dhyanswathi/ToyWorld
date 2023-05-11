using System.ComponentModel.DataAnnotations;

namespace ToyWorld.API.DTO
{
    public class ToyRequest
    {
        [Required]
        public string? Name { get; set; }
        [Required, MaxLength(200)]
        public string? Description { get; set; }

    }
}
