namespace POS.Domain.Entities;

public class PurchaseDetail
{
    public int PurchaseId { get; set; }
    public int ProductServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }

    public virtual Purchase Purchase { get; set; } = null!;
    public virtual ProductService ProductService { get; set; } = null!;
}