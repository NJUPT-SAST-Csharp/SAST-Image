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
            services.Configure<StorageOptions>(configuration.GetRequiredSection("Storage"));

            services.AddScoped<IAccessChecker, AccessChecker>();
            services.AddSingleton<ICompressProcessor, LocalCompressProcessor>();
            services.AddSingleton<IImageFileManager, LocalImageFileManager>();

            services.AddDbContextPool<StorageDbContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("Storage"))
                    .UseSnakeCaseNamingConvention()
            );

            services.AddMediator(options =>
            {
                options.GenerateTypesAsInternal = true;
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });

            return services;
        }
    }
}
