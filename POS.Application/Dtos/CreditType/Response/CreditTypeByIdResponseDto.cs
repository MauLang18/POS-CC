namespace POS.Application.Dtos.CreditType.Response;

public class CreditTypeByIdResponseDto
{
    public int CreditTypeId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int State { get; set; }
}