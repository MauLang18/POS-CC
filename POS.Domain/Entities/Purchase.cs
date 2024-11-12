namespace POS.Domain.Entities;

public class Purchase : BaseEntity
{
    public int SupplierId { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
}