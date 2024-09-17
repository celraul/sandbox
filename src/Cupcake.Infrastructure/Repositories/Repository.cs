using Cupcake.Application.Interfaces;
using Cupcake.Domain.Entities;
using Cupcake.Resilience.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Polly;
using Polly.Registry;
using System.Linq.Expressions;

namespace Cupcake.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection;
    private readonly ResiliencePipeline _resiliencePipeline;

    public Repository(IMongoCupcakeClient mongoClient, ResiliencePipelineProvider<string> pipelineProvider)
    {
        _resiliencePipeline = pipelineProvider.GetPipeline(ResilienceConsts.MongoPipelineName);
        _collection = mongoClient.GetCollection<T>();
    }

    public async Task<T> GetById(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));

        return await _resiliencePipeline.ExecuteAsync(async _ => await _collection.Find(filter).FirstOrDefaultAsync(), cancellationToken);
    }

    public async Task<List<T>> GetPaginated(Expression<Func<T, bool>> filter, int skip, int take, CancellationToken cancellationToken = default)
    {
        List<T> entities = await _resiliencePipeline.ExecuteAsync(async _ =>
            await _collection.Find(filter)
                .Skip(skip).Limit(take).ToListAsync(), cancellationToken);

        return entities;
    }

    public async Task<(List<T>, long)> GetPaginatedWithTotal(Expression<Func<T, bool>> filter, int skip, int take, CancellationToken cancellationToken = default)
    {
        long total = await _resiliencePipeline.ExecuteAsync(async _ => await _collection.CountDocumentsAsync(filter), cancellationToken);

        List<T> entities = await GetPaginated(filter, skip, take, cancellationToken);

        return (entities, total);
    }

    public async Task<IEnumerable<T>> GetByFilter(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _resiliencePipeline.ExecuteAsync(async _ => await _collection.Find(filter).ToListAsync(), cancellationToken);
    }

    public async Task InsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _resiliencePipeline.ExecuteAsync(async _ => await _collection.InsertOneAsync(entity), cancellationToken);
    }

    public async Task<T> InsertAndReturnAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _resiliencePipeline.ExecuteAsync(async _ => await _collection.InsertOneAsync(entity), cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(entity.Id));

        await _resiliencePipeline.ExecuteAsync(async _ => await _collection.ReplaceOneAsync(filter, entity), cancellationToken);
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        DeleteResult result = await _resiliencePipeline.ExecuteAsync(async _ => await _collection.DeleteOneAsync(filter), cancellationToken);

        return result.DeletedCount > 0;
    }
}
