namespace POS.Domain.Entities;

public class QuoteDetail
{
    public int QuoteId { get; set; }
    public int ProductServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }

    public virtual Quote Quote { get; set; } = null!;
    public virtual ProductService ProductService { get; set; } = null!;
}