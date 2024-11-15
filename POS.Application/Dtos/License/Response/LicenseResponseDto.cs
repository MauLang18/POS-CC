namespace POS.Application.Dtos.License.Response;

public class LicenseResponseDto
{
    public int LicenseId { get; set; }
    public string? LicenseKey { get; set; }
    public string? Project { get; set; }
    public string? LicenseType { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateLicense { get; set; }
}