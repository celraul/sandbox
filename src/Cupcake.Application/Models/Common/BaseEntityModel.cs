namespace Cupcake.Application.Models.Common;

public class BaseEntityModel
{
    public string Id { get; set; } = null!;
    public UserModel CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public UserModel UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
