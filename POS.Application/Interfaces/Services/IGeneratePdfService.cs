namespace POS.Application.Interfaces.Services;

public interface IGeneratePdfService
{
    Task<byte[]> GeneratePdf<T>(T data, int templateId);
}