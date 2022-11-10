namespace ToyWorld.API.Models
{
    public class ToyRepository : IToyRepository
    {
        private readonly ToysDBContext _context;
        public ToyRepository(ToysDBContext context)
        {
            _context = context;
        }
        public int Add(Toy item)
        {
            _context.Add(item);
            Save();
            return item.Id;
        }

        public void Delete(int id)
        {
            var item = GetById(id);
            if(item != null)
            {
                 _context.Remove(item);
                  Save();
            }
        }

        public IEnumerable<Toy> GetAll()
        {
            return _context.Toy;
        }

        public Toy? GetById(int id)
        {
            return _context.Toy.FirstOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Toy item)
        {
            _context.Update(item);
            Save();
        }
    }

}
