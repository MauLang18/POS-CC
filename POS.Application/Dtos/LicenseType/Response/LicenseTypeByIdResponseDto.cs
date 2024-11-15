namespace POS.Application.Dtos.LicenseType.Response;

public class LicenseTypeByIdResponseDto
{
    public int LicenseTypeId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int State { get; set; }
}