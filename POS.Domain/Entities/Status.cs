namespace POS.Domain.Entities;

public class Status : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}