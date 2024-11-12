namespace POS.Domain.Entities;

public class SaleDetail
{
    public int SaleId { get; set; }
    public int ProductServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }

    public virtual Sale Sale { get; set; } = null!;
    public virtual ProductService ProductService { get; set; } = null!;
}