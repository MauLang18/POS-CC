namespace POS.Infrastructure.Services;

public class EmailSettings
{
    public const string SectionName = "Email";
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? UserName { get; set; }
    public string? CC { get; set; }
    public string? PassWord { get; set; }
}