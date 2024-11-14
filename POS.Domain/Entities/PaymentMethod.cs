namespace POS.Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}