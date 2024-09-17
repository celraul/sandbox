using Cupcake.Domain.Enums;

namespace Cupcake.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public CupcakeType Type { get; set; }
    public decimal CostPrice { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Pictures { get; set; } = [];
}
