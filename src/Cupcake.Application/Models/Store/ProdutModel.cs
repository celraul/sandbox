using Cupcake.Application.Models.Common;
using Cupcake.Domain.Enums;

namespace Cupcake.Application.Models.Store;

public class ProdutModel : BaseEntityModel
{
    public string Name { get; set; } = string.Empty;
    public CupcakeType Type { get; set; }
    public decimal CostPrice { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<string> Pictures { get; set; } = [];
}
