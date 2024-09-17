namespace Cupcake.Infrastructure.Options;

public class MongoSettings
{
    public string ConnectionString { get; set; } = string.Empty; // TODO use azure key
    public string DataBaseName { get; set; } = string.Empty; // TODO use azure key
}
