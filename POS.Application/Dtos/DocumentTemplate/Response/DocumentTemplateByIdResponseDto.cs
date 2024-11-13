namespace POS.Application.Dtos.DocumentTemplate.Response;

public class DocumentTemplateByIdResponseDto
{
    public int DocumentTemplateId { get; set; }
    public string? Name { get; set; }
    public int TemplateTypeId { get; set; }
    public string? Content { get; set; }
    public int State { get; set; }
}