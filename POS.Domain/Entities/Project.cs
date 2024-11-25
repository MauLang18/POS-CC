namespace POS.Domain.Entities;

public class Project : BaseEntity
{
    public string InternalName { get; set; } = null!;
    public string CommercialName { get; set; } = null!;
    public int CustomerId { get; set; }
    public int CategoryId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusId { get; set; }

    public virtual Customer Customer { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
    public virtual Status Status { get; set; } = null!;
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public virtual ICollection<License> Licenses { get; set; } = new List<License>();
    public virtual ICollection<ProjectDetail> ProjectDetails { get; set; } = new List<ProjectDetail>();
}