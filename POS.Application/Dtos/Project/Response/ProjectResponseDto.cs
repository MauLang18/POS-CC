namespace POS.Application.Dtos.Project.Response;

public class ProjectResponseDto
{
    public int ProjectId { get; set; }
    public string? InternalName { get; set; }
    public string? CommercialName { get; set; }
    public string? Customer { get; set; }
    public string? Category { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public string? Status { get; set; }
    public int State { get; set; }
    public string? StateProject { get; set; }
}