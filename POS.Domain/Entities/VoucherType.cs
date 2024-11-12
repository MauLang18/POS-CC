namespace POS.Domain.Entities;

public class VoucherType : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;

    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}