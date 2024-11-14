namespace POS.Domain.Entities;

public class Customer : BaseEntity
{
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string ContactName { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int CreditTypeId { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal CreditInterestRate { get; set; }
    public decimal CreditLimit { get; set; }

    public virtual DocumentType DocumentType { get; set; } = null!;
    public virtual CreditType CreditType { get; set; } = null!;
    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}