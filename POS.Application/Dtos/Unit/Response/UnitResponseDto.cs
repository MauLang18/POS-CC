namespace POS.Application.Dtos.Unit.Response;

public class UnitResponseDto
{
    public int UnitId { get; set; }
    public string? Name { get; set; }
    public string? Abbreviation { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateUnit { get; set; }
}