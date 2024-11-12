namespace POS.Domain.Entities;

public class DocumentType : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}