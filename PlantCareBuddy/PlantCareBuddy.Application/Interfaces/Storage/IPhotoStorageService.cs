using Microsoft.AspNetCore.Http;

namespace PlantCareBuddy.Application.Interfaces.Storage
{
    public interface IPhotoStorageService
    {
        /// <summary>
        /// Stores a photo and returns the path/URL
        /// </summary>
        Task<string> StorePhotoAsync(IFormFile photo, string subfolder = "plants");

        /// <summary>
        /// Generates a thumbnail from a photo and returns the path/URL
        /// </summary>
        Task<string> GenerateThumbnailAsync(string photoPath, int width = 200, int height = 200);

        /// <summary>
        /// Deletes a photo from storage
        /// </summary>
        Task<bool> DeletePhotoAsync(string photoPath);

        /// <summary>
        /// Validates if a file is a valid image with allowed extensions
        /// </summary>
        bool ValidatePhoto(IFormFile file);

        /// <summary>
        /// Gets the full URL for a photo path
        /// </summary>
        string GetPhotoUrl(string photoPath);
    }
}