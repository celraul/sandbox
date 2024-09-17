using Cupcake.Resilience.Configuration;
using Cupcake.Resilience.Pipelines;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Cupcake.Resilience;

public static class ConfigureServices
{
    public static IServiceCollection AddResilience(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoResilience(configuration);

        return services;
    }

    private static IServiceCollection AddMongoResilience(this IServiceCollection services, IConfiguration configuration)
    {
        ResilienceConfiguration resilienceConfiguration = GetResilienceConfiguration(configuration);

        services.AddResiliencePipeline(ResilienceConsts.MongoPipelineName,
            (builder, context) =>
            {
                builder.AddPipeline(MongoPipeline.GetMongoPipeline(ResilienceConsts.DefaultRetryCount));
            });

        return services;
    }

    private static ResilienceConfiguration GetResilienceConfiguration(IConfiguration? configuration)
    {
        return configuration?.GetSection(ResilienceConfiguration.DefaultSection).Get<ResilienceConfiguration>()
                 ?? new ResilienceConfiguration();
    }
}
