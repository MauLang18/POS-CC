namespace POS.Application.Dtos.TemplateType.Response;

public class TemplateTypeByIdResponseDto
{
    public int TemplateTypeId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int State { get; set; }
}