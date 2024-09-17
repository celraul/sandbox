using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cupcake.Resilience;
using Cupcake.Infrastructure.Repositories;
using Cupcake.Infrastructure.Options;
using Cupcake.Infrastructure.Mongo;
using MongoDB.Bson.Serialization;
using Cupcake.Domain.Entities;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using Cupcake.Application.Interfaces;

namespace Cupcake.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongo(configuration)
            .AddResilience(configuration)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    private static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoSettings>(option => configuration.GetSection(nameof(MongoSettings)).Bind(option));

        services.AddSingleton<IMongoCupcakeClient, MongoCupcakeClient>();

        BsonClassMap.RegisterClassMap<BaseEntity>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapIdProperty(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
        });

        var ingnoreExtraElements = new ConventionPack { new IgnoreExtraElementsConvention(true) };
        ConventionRegistry.Register("Ignore extra elements", ingnoreExtraElements, t => true);

        return services;
    }
}
