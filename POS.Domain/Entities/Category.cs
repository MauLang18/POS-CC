namespace POS.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<ProductService> ProductServices { get; set; } = new List<ProductService>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}