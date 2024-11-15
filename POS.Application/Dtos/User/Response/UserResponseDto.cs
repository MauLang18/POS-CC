namespace POS.Application.Dtos.User.Response;

public class UserResponseDto
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateUser { get; set; }
}