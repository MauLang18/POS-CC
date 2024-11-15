namespace POS.Application.Dtos.Purchase.Response;

public class PurchaseDetailsByIdResponseDto
{
    public int ProductServiceId { get; set; }
    public string? Image { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}