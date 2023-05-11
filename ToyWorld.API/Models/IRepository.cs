namespace ToyWorld.API.Models
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(Guid id);
        Guid Add(T item);
        void Update(T item);
        void Delete(Guid id);
        void Save();
    }
}
