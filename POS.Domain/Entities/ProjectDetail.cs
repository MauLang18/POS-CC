namespace POS.Domain.Entities;

public class ProjectDetail
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Requirement { get; set; } = null!;
    public int StateId { get; set; }

    public virtual Project Project { get; set; } = null!;
    public virtual Status Status { get; set; } = null!;
}