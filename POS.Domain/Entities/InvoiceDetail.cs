namespace POS.Domain.Entities;

public class InvoiceDetail
{
    public int InvoiceId { get; set; }
    public int ProductServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
    public virtual ProductService ProductService { get; set; } = null!;
}