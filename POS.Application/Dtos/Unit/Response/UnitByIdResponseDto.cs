namespace POS.Application.Dtos.Unit.Response;

public class UnitByIdResponseDto
{
    public int UnitId { get; set; }
    public string? Name { get; set; }
    public string? Abbreviation { get; set; }
    public int State { get; set; }
}