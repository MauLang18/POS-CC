namespace POS.Application.Dtos.CreditType.Response;

public class CreditTypeResponseDto
{
    public int CreditTypeId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateCreditType { get; set; }
}