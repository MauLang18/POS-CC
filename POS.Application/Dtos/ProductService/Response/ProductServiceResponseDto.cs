namespace POS.Application.Dtos.ProductService.Response;

public class ProductServiceResponseDto
{
    public int ProductServiceId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Category { get; set; }
    public string? Unit { get; set; }
    public int IsService { get; set; }
    public string? Service { get; set; }
    public int StockQuantity { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateProductService { get; set; }
}