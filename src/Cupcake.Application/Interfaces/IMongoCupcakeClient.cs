using MongoDB.Driver;

namespace Cupcake.Application.Interfaces;

public interface IMongoCupcakeClient
{
    IMongoCollection<T> GetCollection<T>();
}
