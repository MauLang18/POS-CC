namespace POS.Application.Dtos.Status.Response;

public class StatusResponseDto
{
    public int StatusId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateStatus { get; set; }
}