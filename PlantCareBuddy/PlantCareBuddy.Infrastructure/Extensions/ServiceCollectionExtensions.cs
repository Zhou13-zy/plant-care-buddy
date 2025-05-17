using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantCareBuddy.Infrastructure.Config;
using PlantCareBuddy.Infrastructure.Interfaces.Storage; // or wherever your interface is
using PlantCareBuddy.Infrastructure.Services.Storage;

namespace PlantCareBuddy.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the photo storage service and its configuration.
        /// </summary>
        public static IServiceCollection AddPhotoStorage(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register configuration settings
            services.Configure<PhotoStorageSettings>(
                configuration.GetSection("PhotoStorage"));

            // Register the photo storage service
            services.AddScoped<IPhotoStorageService, FileSystemPhotoStorage>();

            return services;
        }
    }
}