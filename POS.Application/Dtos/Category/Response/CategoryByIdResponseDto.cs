namespace POS.Application.Dtos.Category.Response;

public class CategoryByIdResponseDto
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int State { get; set; }
}