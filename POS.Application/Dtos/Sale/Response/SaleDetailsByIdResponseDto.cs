namespace POS.Application.Dtos.Sale.Response;

public class SaleDetailsByIdResponseDto
{
    public int ProductServiceId { get; set; }
    public string? Image { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
}