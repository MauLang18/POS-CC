namespace POS.Application.Dtos.DocumentType.Response;

public class DocumentTypeByIdResponseDto
{
    public int DocumentTypeId { get; set; }
    public string? Name { get; set; }
    public string? Abbreviation { get; set; }
    public int State { get; set; }
}