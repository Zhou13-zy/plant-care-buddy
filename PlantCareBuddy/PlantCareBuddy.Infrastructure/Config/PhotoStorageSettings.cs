namespace PlantCareBuddy.Infrastructure.Config
{
    public class PhotoStorageSettings
    {
        public string StoragePath { get; set; } = "wwwroot/images";
        public string[] AllowedExtensions { get; set; } = { ".jpg", ".jpeg", ".png" };
        public long MaxFileSize { get; set; } = 5 * 1024 * 1024; // 5MB
        public string BaseUrl { get; set; } = "/images";
    }
}