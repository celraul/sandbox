using Cupcake.Resilience.Configuration;
using MongoDB.Driver;
using Polly;

namespace Cupcake.Resilience.Pipelines;

internal static class MongoPipeline
{
    public static ResiliencePipeline GetMongoPipeline(int retryCount)
    {
        var shouldHandle = new PredicateBuilder().Handle<TimeoutException>()
            .Handle<MongoConnectionException>();

        return BasePipeline.GetDefaultRetryStrategy(shouldHandle, ResilienceConsts.MongoPipelineName, retryCount);
    }
}
