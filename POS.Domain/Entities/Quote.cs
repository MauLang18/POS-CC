namespace POS.Domain.Entities;

public class Quote : BaseEntity
{
    public int VoucherTypeId { get; set; }
    public string VoucherNumber { get; set; } = null!;
    public int CustomerId { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public int StatusId { get; set; }

    public virtual VoucherType VoucherType { get; set; } = null!;
    public virtual Customer Customer { get; set; } = null!;
    public virtual Status Status { get; set; } = null!;
    public virtual ICollection<QuoteDetail> QuoteDetails { get; set; } = new List<QuoteDetail>();
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}