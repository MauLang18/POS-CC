namespace POS.Domain.Entities;

public class ProductService : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int UnitId { get; set; }
    public int IsService { get; set; }
    public int StockQuantity { get; set; }

    public virtual Category Category { get; set; } = null!;
    public virtual Unit Unit { get; set; } = null!;
    public virtual ICollection<QuoteDetail> QuoteDetails { get; set; } = new List<QuoteDetail>();
    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
    public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new List<PurchaseDetail>();
}