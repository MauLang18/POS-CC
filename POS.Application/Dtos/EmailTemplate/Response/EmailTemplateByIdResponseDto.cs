namespace POS.Application.Dtos.EmailTemplate.Response;

public class EmailTemplateByIdResponseDto
{
    public int EmailTemplateId { get; set; }
    public int TemplateTypeId { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public int State { get; set; }
}