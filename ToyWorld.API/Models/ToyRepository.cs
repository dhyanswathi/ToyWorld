using Microsoft.EntityFrameworkCore;
using ToyWorld.API.DTO;

namespace ToyWorld.API.Models
{
    public class ToyRepository : IToyRepository
    {
        private readonly ToysDBContext _context;
        public ToyRepository(ToysDBContext context)
        {
            _context = context;
        }

        public async Task<Toy> CreateToy(ToyRequest request)
        {
            var toy = new Toy()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                UserId = request.UserId,
            };

            await _context.Toys.AddAsync(toy);
            await SaveToy();
            return toy;
        }

        public async Task DeleteToy(Guid id)
        {
            var toy = await GetToy(id);
            _context.Toys.Remove(toy);
            await SaveToy();
        }

        public async Task<List<Toy>> GetAllToys()
        {
            var toys = await _context.Toys.ToListAsync();
            return toys;
        }

        public async Task<Toy?> GetToy(Guid id)
        {
            return await _context.Toys.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task SaveToy()
        {
           await _context.SaveChangesAsync();
        }

        public Byte[] AddImage(IFormFile model)
        {
            var stream = new MemoryStream();
            model.CopyTo(stream);

            return stream.ToArray();
        }

        public async Task UploadImage(FileUploadModel model, Guid id)
        {
            var toy = await GetToy(id);
           var stream = AddImage(model.FileDetails);
            toy.Image = stream;
            await SaveToy();

        }
    }

}
