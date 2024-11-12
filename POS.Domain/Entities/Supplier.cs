namespace POS.Domain.Entities;

public class Supplier : BaseEntity
{
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ContactName { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }

    public virtual DocumentType DocumentType { get; set; } = null!;
    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}