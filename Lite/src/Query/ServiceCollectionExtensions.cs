using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Query.Database;

namespace Query;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Adds query database context services to the dependency injection container using the specified
        /// configuration.
        /// </summary>
        /// <param name="configuration">IConfigurationRoot["Query"]</param>
        public IServiceCollection AddQueryServices(IConfiguration configuration)
        {
            services.AddDistributedPostgresCache(options =>
            {
                options.ConnectionString = configuration.GetConnectionString("Cache");
                options.CreateIfNotExists = true;
                options.TableName = "cache";
                options.SchemaName = "query";
            });

            services.AddDbContext<QueryDbContext>(
                (provider, options) =>
                    options
                        .UseNpgsql(provider.GetRequiredService<DbConnection>())
                        .UseModel(QueryDbContextModel.Instance)
                        .UseSnakeCaseNamingConvention()
            );
            return services;
        }
    }
}
