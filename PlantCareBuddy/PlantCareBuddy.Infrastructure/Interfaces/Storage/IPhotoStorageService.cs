using Microsoft.AspNetCore.Http;

namespace PlantCareBuddy.Infrastructure.Interfaces.Storage
{
    public interface IPhotoStorageService
    {
        Task<string> StorePhotoAsync(IFormFile photo, string subfolder = "plants");
        Task<string> GenerateThumbnailAsync(string photoPath, int width = 200, int height = 200);
        Task<bool> DeletePhotoAsync(string photoPath);
        bool ValidatePhoto(IFormFile file);
        string GetPhotoUrl(string photoPath);
    }
}