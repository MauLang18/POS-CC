namespace POS.Application.Dtos.ProductService.Response;

public class ProductServiceByIdResponseDto
{
    public int ProductServiceId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int UnitId { get; set; }
    public int IsService { get; set; }
    public int StockQuantity { get; set; }
    public int State { get; set; }
}