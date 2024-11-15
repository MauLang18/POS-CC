namespace POS.Application.Dtos.License.Response;

public class LicenseByIdResponseDto
{
    public int LicenseId { get; set; }
    public string? LicenseKey { get; set; }
    public int ProjectId { get; set; }
    public int LicenseTypeId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int State { get; set; }
}