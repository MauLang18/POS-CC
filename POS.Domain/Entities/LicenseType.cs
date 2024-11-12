namespace POS.Domain.Entities;

public class LicenseType : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
}