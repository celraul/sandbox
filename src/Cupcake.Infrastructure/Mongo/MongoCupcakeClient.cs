using Cupcake.Application.Interfaces;
using Cupcake.Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Cupcake.Infrastructure.Mongo;

public class MongoCupcakeClient : IMongoCupcakeClient
{
    private readonly IMongoClient _mongoClient;
    private readonly MongoSettings _mongoSettings;

    public MongoCupcakeClient(IOptions<MongoSettings> options)
    {
        if (options.Value is null)
            throw new ArgumentNullException(nameof(options));

        _mongoSettings = options.Value;

        _mongoClient = new MongoClient(_mongoSettings.ConnectionString);
    }

    public IMongoCollection<T> GetCollection<T>()
    {
        IMongoDatabase mongoDatabase = _mongoClient.GetDatabase(_mongoSettings.DataBaseName);

        return mongoDatabase.GetCollection<T>(typeof(T).Name);
    }
}
