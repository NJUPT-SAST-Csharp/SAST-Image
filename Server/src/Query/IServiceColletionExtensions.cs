using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Query.Database;

namespace Query;

public static class IServiceColletionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddQuery(IConfiguration configuration)
        {
            services.AddDistributedPostgresCache(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("Cache");
                options.CreateIfNotExists = true;
                options.TableName = "cache";
                options.SchemaName = "query";
            });

            services.AddDbContextFactory<QueryDbContext>(options =>
                options
                    .UseNpgsql(configuration.GetConnectionString("Query"))
                    .UseSnakeCaseNamingConvention()
            );
            services.AddMediator(options =>
            {
                options.GenerateTypesAsInternal = true;
                options.ServiceLifetime = ServiceLifetime.Scoped;
            });
            services.AddHostedService<QueryService>();

            return services;
        }
    }
}
