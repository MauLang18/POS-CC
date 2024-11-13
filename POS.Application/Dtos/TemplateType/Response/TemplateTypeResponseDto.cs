namespace POS.Application.Dtos.TemplateType.Response;

public class TemplateTypeResponseDto
{
    public int TemplateTypeId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateTemplateType { get; set; }
}