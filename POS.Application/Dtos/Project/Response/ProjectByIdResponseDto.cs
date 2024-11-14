namespace POS.Application.Dtos.Project.Response;

public class ProjectByIdResponseDto
{
    public int ProjectId { get; set; }
    public string? InternalName { get; set; }
    public string? CommercialName { get; set; }
    public int CustomerId { get; set; }
    public int CategoryId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusId { get; set; }
    public int State { get; set; }
}