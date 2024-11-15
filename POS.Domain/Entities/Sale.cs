namespace POS.Domain.Entities;

public class Sale : BaseEntity
{
    public int VoucherTypeId { get; set; }
    public string VoucherNumber { get; set; } = null!;
    public int CustomerId { get; set; }
    public int? QuoteId { get; set; }
    public int? ProjectId { get; set; }
    public int? PaymentMethodId {  get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public int StatusId { get; set; }

    public virtual VoucherType VoucherType { get; set; } = null!;
    public virtual Customer Customer { get; set; } = null!;
    public virtual Quote Quote { get; set; } = null!;
    public virtual Project Project { get; set; } = null!;
    public virtual Status Status { get; set; } = null!;
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}