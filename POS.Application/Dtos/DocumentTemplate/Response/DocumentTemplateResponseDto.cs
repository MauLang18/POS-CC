namespace POS.Application.Dtos.DocumentTemplate.Response;

public class DocumentTemplateResponseDto
{
    public int DocumentTemplateId { get; set; }
    public string? Name { get; set; }
    public string? TemplateType { get; set; }
    public string? Content { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateDocumentTemplate { get; set; }
}