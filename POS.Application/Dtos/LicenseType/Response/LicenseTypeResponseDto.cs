namespace POS.Application.Dtos.LicenseType.Response;

public class LicenseTypeResponseDto
{
    public int LicenseTypeId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateLicenseType { get; set; }
}