using Domain.Filters;
using Domain.Services;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Serialization;

namespace Domain;

public static class ISiloBuilderExtensions
{
    extension(ISiloBuilder builder)
    {
        public ISiloBuilder UseDomain()
        {
            builder.AddCustomStorageBasedLogConsistencyProviderAsDefault();

            builder.AddOutgoingGrainCallFilter<AccessControlFilter>();
            builder.AddOutgoingGrainCallFilter<EnsureExistsFilter>();
            builder.AddOutgoingGrainCallFilter<EnsureUniqueIdFilter>();
            builder.Services.AddSerializer(b =>
                b.AddJsonSerializer(type => type.Namespace!.StartsWith("Domain"))
            );

            builder.Services.AddSingleton<IUsernameUniquenessChecker, UsernameUniquenessChecker>();
            builder.Services.AddSingleton<IIdUniquenessChecker, IdUniquenessChecker>();

            builder.Services.AddDbContextFactory<DomainDbContext>(options =>
                options
                    .UseNpgsql(
                        builder.Configuration.GetConnectionString("Domain"),
                        options =>
                            options.ConfigureDataSource(b =>
                                b.EnableDynamicJson()
                                    .ConfigureJsonOptions(
                                        new() { AllowOutOfOrderMetadataProperties = true }
                                    )
                            )
                    )
                    .UseSnakeCaseNamingConvention()
            );

            return builder;
        }
    }
}
