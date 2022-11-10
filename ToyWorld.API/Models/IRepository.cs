namespace ToyWorld.API.Models
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        int Add(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
