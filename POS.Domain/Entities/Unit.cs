namespace POS.Domain.Entities;

public class Unit : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;

    public virtual ICollection<ProductService> ProductServices { get; set; } = new List<ProductService>();
}