namespace POS.Application.Interfaces.Services;

public interface IGenerateCodeService
{
    Task<string> GenerateCodeProduct(int categoryId);
    Task<string> GenerateCodeInvoice(int invoiceId);
    Task<string> GenerateCodeSale(int saleId);
    Task<string> GenerateCodeQuote(int quoteId);
    Task<string> GenerateSoftwareLicense(int projectId, int licenseTypeId);
}