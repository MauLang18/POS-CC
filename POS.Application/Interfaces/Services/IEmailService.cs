namespace POS.Application.Interfaces.Services;

public interface IEmailService
{
    Task SendEmailWithAttachmentAsync(string recipientEmail, byte[] pdfBytes, string subject, string htmlTemplate);
}