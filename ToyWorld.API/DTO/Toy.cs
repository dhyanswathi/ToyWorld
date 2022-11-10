using System.ComponentModel.DataAnnotations;

namespace ToyWorld.API.DTO
{
    public class Toy
    {
        [Required]
        public string? Name { get; set; }
        [Required, MaxLength(200)]
        public string? Description { get; set; }
        [Required, MaxLength(100)]
        public string? ImageUrl { get; set; }
    }
}
