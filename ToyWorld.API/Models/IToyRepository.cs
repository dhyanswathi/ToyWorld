using ToyWorld.API.DTO;

namespace ToyWorld.API.Models
{
    public interface IToyRepository 
    {
        void SaveToy();
        IEnumerable<ToyResponse> GetAllToys();
        Toy? GetToy(Guid id);
        Toy CreateToy(ToyRequest request);
        void DeleteToy(Guid id);
        void UploadImage(FileUploadModel model, Guid id);

    }
}
