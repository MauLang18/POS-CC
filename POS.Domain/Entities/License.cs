namespace POS.Domain.Entities;

public class License : BaseEntity
{
    public string LicenseKey { get; set; } = null!;
    public int ProjectId { get; set; }
    public int LicenseTypeId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }

    public virtual Project Project { get; set; } = null!;
    public virtual LicenseType LicenseType { get; set; } = null!;
}