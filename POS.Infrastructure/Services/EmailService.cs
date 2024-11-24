using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Newtonsoft.Json;
using POS.Application.Commons.Config;
using POS.Application.Interfaces.Services;

namespace POS.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly EmailSettings _emailSettings;

    public EmailService(IUnitOfWork unitOfWork, IVaultSecretService vaultSecretService)
    {
        _unitOfWork = unitOfWork;

        var secretJson = vaultSecretService.GetSecret("CustomCodeAPI/data/Email").GetAwaiter().GetResult();
        var secretResponse = JsonConvert.DeserializeObject<SecretResponse<EmailSettings>>(secretJson);

        if (secretResponse?.Data?.Data == null)
        {
            throw new Exception("Failed to retrieve email settings from Vault.");
        }

        _emailSettings = secretResponse.Data.Data;
    }

    public async Task SendEmail<T>(T data, int templateId, byte[] pdfBytes, string customer, string pdf)
    {
        var template = await _unitOfWork.EmailTemplate.GetByIdAsync(templateId);
        if (template == null || string.IsNullOrEmpty(template.Body) || string.IsNullOrEmpty(template.Subject))
            throw new Exception("No se pudo cargar la plantilla de correo o el asunto.");

        var populatedBody = PopulateTemplate(template.Body, data) ?? string.Empty;
        var populatedSubject = PopulateTemplate(template.Subject, data) ?? "Sin Asunto";

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_emailSettings.UserName ?? throw new InvalidOperationException("El nombre de usuario no está configurado.")));
        email.To.Add(MailboxAddress.Parse(customer ?? throw new ArgumentNullException(nameof(customer), "El correo del cliente no puede ser nulo o vacío.")));

        // Agregar CC si está configurado en _emailSettings
        if (!string.IsNullOrEmpty(_emailSettings.CC))
        {
            email.Cc.Add(MailboxAddress.Parse(_emailSettings.CC));
        }

        email.Subject = populatedSubject;

        var builder = new BodyBuilder { HtmlBody = populatedBody };

        if (pdfBytes != null && pdfBytes.Length > 0)
        {
            builder.Attachments.Add($"{pdf}.pdf", pdfBytes, ContentType.Parse("application/pdf"));
        }

        email.Body = builder.ToMessageBody();

        using var smtpClient = new SmtpClient();
        await smtpClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
        smtpClient.Authenticate(_emailSettings.UserName, _emailSettings.PassWord);

        await smtpClient.SendAsync(email);
        await smtpClient.DisconnectAsync(true);
    }

    private string PopulateTemplate<T>(string templateContent, T data)
    {
        string populatedContent = templateContent ?? string.Empty;

        foreach (var property in typeof(T).GetProperties())
        {
            var key = "{{" + property.Name + "}}";
            var value = property.GetValue(data)?.ToString();
            if (value != null)
            {
                populatedContent = populatedContent.Replace(key, value);
            }
        }

        return populatedContent;
    }
}