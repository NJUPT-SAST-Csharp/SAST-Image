using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Database;
using Storage.Services;

namespace Storage;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddStorage(IConfiguration configuration)
        {
            services.AddHostedService<StorageService>();

            services.Configure<StorageOptions>(configuration.GetRequiredSection("Storage"));

            services.AddSingleton<IAccessChecker, AccessChecker>();
            services.AddSingleton<ICompressProcessor, LocalCompressProcessor>();
            services.AddSingleton<IImageFileManager, LocalImageFileManager>();

            services.AddDbContextFactory<StorageDbContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("Storage"))
                    .UseSnakeCaseNamingConvention()
            );

            services.AddMediator(options =>
            {
                options.GenerateTypesAsInternal = true;
            });

            return services;
        }
    }
}
