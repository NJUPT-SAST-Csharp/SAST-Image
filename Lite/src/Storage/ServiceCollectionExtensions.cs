using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Albums;
using Storage.Database;
using Storage.Images;

namespace Storage;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddStorageServices<TAlbumChecker, TImageChecker>(
            IConfigurationRoot configuration
        )
            where TAlbumChecker : class, IAlbumAvailabilityChecker
            where TImageChecker : class, IImageAvailabilityChecker
        {
            services.AddHostedService<StorageService>();

            services.Configure<StorageOptions>(configuration.GetRequiredSection("Storage"));

            services.AddSingleton<IImageFileManager, LocalImageFileManager>();
            services.AddSingleton<ICompressProcessor, LocalCompressProcessor>();

            services.AddScoped<IAlbumAvailabilityChecker, TAlbumChecker>();
            services.AddScoped<IImageAvailabilityChecker, TImageChecker>();

            services.AddDbContext<StorageDbContext>(
                (provider, options) =>
                    options
                        .UseNpgsql(provider.GetRequiredService<DbConnection>())
                        .UseSnakeCaseNamingConvention()
            );
            return services;
        }
    }
}
