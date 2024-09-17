namespace Cupcake.Domain.Entities;

public class BaseEntity
{
    public string Id { get; set; } = null!;
    public User CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public User? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
