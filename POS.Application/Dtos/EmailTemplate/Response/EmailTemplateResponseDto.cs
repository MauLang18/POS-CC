namespace POS.Application.Dtos.EmailTemplate.Response;

public class EmailTemplateResponseDto
{
    public int EmailTemplateId { get; set; }
    public string? TemplateType { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateEmailTemplate { get; set; }
}