namespace POS.Application.Dtos.Status.Response;

public class StatusByIdResponseDto
{
    public int StatusId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int State { get; set; }
}