namespace Cupcake.Resilience.Configuration;

public static class ResilienceConsts
{
    public const int DefaultRetryCount = 3;
    public const int DefaultDelay = 3;
    public const string MongoPipelineName = "MongoResiliencePipeline";
}
