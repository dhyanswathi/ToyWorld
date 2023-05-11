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

        public Toy CreateToy(ToyRequest request)
        {
            var toy = new Toy()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                UserId = request.UserId,
            };

            _context.Toys.Add(toy);
            SaveToy();
            return toy;
        }

        public void DeleteToy(Guid id)
        {
            var toy = GetToy(id);
            _context.Toys.Remove(toy);
        }

        public IEnumerable<ToyResponse> GetAllToys()
        {
            return _context.Toys.Select(x => new ToyResponse() 
            { 
                Id = x.Id, 
                Name = x.Name, 
                Description = x.Description,
                Image = x.Image
            }).ToList();
        }

        public Toy? GetToy(Guid id)
        {
            return _context.Toys.FirstOrDefault(x => x.Id == id);
        }

        public void SaveToy()
        {
            _context.SaveChanges();
        }

        public Byte[] AddImage(IFormFile model)
        {
            var stream = new MemoryStream();
            model.CopyTo(stream);

            return stream.ToArray();
        }

        public void UploadImage(FileUploadModel model, Guid id)
        {
            var toy = GetToy(id);
           var stream = AddImage(model.FileDetails);
            toy.Image = stream;
            SaveToy();

        }
    }

}
