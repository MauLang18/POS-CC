namespace POS.Application.Interfaces.Services;

public interface IGeneratePdfService
{
    byte[] GeneratePdf<T>(T data, string templatePath);
}