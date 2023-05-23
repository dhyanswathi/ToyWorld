using ToyWorld.API.DTO;

namespace ToyWorld.API.Models
{
    public interface IToyRepository 
    {
        Task SaveToy();
        Task<List<ToyResponse>> GetAllToys();
        Task<Toy?> GetToy(Guid id);
        Task<Toy> CreateToy(ToyRequest request);
        Task DeleteToy(Guid id);
        Task UploadImage(FileUploadModel model, Guid id);

    }
}
