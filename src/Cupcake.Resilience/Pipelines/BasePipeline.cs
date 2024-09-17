using Cupcake.Resilience.Configuration;
using Polly;
using Polly.Retry;

namespace Cupcake.Resilience.Pipelines;

internal static class BasePipeline
{
    public static ResiliencePipeline GetDefaultRetryStrategy(Func<RetryPredicateArguments<object>, ValueTask<bool>> shouldHandle,
        string pipelineName, int retryCount)
    {
        return new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions()
            {
                BackoffType = DelayBackoffType.Exponential,
                Delay = TimeSpan.FromSeconds(ResilienceConsts.DefaultDelay),
                MaxRetryAttempts = retryCount,
                ShouldHandle = shouldHandle,
                OnRetry = retryArguments =>
                {
                    int attemptNumber = retryArguments.AttemptNumber + 1;
                    Console.WriteLine(@$"Retry pipeline {pipelineName} {attemptNumber} of {retryCount} due {retryArguments.Outcome}" +
                        $"ex: {retryArguments.Outcome.Exception?.Message} {retryArguments.Outcome.Exception?.InnerException?.Message}");

                    Console.WriteLine($"Retrying in {retryArguments.RetryDelay.Seconds} seconds.");

                    return ValueTask.CompletedTask;
                }
                // TODO Log retry

            }).Build();
    }
}
