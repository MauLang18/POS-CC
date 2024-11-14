namespace POS.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmail<T>(T data, int templateId, byte[] pdfBytes, string customer, string pdf);
}