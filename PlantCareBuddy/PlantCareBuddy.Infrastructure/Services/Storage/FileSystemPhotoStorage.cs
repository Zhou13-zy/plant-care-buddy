using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PlantCareBuddy.Infrastructure.Config;
using PlantCareBuddy.Infrastructure.Interfaces.Storage;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace PlantCareBuddy.Infrastructure.Services.Storage
{
    public class FileSystemPhotoStorage : IPhotoStorageService
    {
        private readonly PhotoStorageSettings _settings;

        public FileSystemPhotoStorage(IOptions<PhotoStorageSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<string> StorePhotoAsync(IFormFile photo, string subfolder = "plants")
        {
            if (photo == null || photo.Length == 0)
                throw new ArgumentException("No file provided");

            if (!ValidatePhoto(photo))
                throw new ArgumentException("Invalid file type or size");

            // Create directory if it doesn't exist
            var directoryPath = Path.Combine(_settings.StoragePath, subfolder);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            var filePath = Path.Combine(directoryPath, fileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            // Return relative path for storage in database
            return Path.Combine(subfolder, fileName).Replace('\\', '/');
        }

        public async Task<string> GenerateThumbnailAsync(string photoPath, int width = 200, int height = 200)
        {
            var fullPath = Path.Combine(_settings.StoragePath, photoPath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException("Original photo not found", fullPath);

            // Generate thumbnail filename and path
            var directory = Path.GetDirectoryName(photoPath);
            var filename = Path.GetFileNameWithoutExtension(photoPath);
            var extension = Path.GetExtension(photoPath);
            var thumbnailFilename = $"{filename}_thumb{extension}";
            var thumbnailPath = Path.Combine(directory, thumbnailFilename).Replace('\\', '/');
            var fullThumbnailPath = Path.Combine(_settings.StoragePath, thumbnailPath);

            // Ensure directory exists
            var thumbnailDir = Path.GetDirectoryName(fullThumbnailPath);
            if (!Directory.Exists(thumbnailDir))
                Directory.CreateDirectory(thumbnailDir);

            // Generate thumbnail using ImageSharp
            using (var image = await Image.LoadAsync(fullPath))
            {
                image.Mutate(x => x.Resize(width, height));
                await image.SaveAsync(fullThumbnailPath);
            }

            return thumbnailPath;
        }

        public Task<bool> DeletePhotoAsync(string photoPath)
        {
            if (string.IsNullOrEmpty(photoPath))
                return Task.FromResult(false);

            var fullPath = Path.Combine(_settings.StoragePath, photoPath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);

                // Also delete thumbnail if it exists
                var directory = Path.GetDirectoryName(photoPath);
                var filename = Path.GetFileNameWithoutExtension(photoPath);
                var extension = Path.GetExtension(photoPath);
                var thumbnailPath = Path.Combine(_settings.StoragePath, directory, $"{filename}_thumb{extension}");

                if (File.Exists(thumbnailPath))
                    File.Delete(thumbnailPath);

                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public bool ValidatePhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            // Check file size
            if (file.Length > _settings.MaxFileSize)
                return false;

            // Check file extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return _settings.AllowedExtensions.Contains(extension);
        }

        public string GetPhotoUrl(string photoPath)
        {
            if (string.IsNullOrEmpty(photoPath))
                return null;

            return $"{_settings.BaseUrl}/{photoPath}";
        }
    }
}