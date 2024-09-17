namespace Cupcake.Resilience.Configuration;

public class ResilienceConfiguration
{
    public const string DefaultSection = nameof(ResilienceConfiguration);

    public int MongoRetryCount { get; set; } = ResilienceConsts.DefaultRetryCount;
    public string MongoPipelineName { get; set; } = "MongoResiliencePipeline";
}
