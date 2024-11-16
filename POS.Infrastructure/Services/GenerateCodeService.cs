using POS.Application.Interfaces.Persistence;
using POS.Application.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace POS.Infrastructure.Services;

public class GenerateCodeService : IGenerateCodeService
{
    private readonly ICodeRepository _codeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GenerateCodeService(ICodeRepository codeRepository, IUnitOfWork unitOfWork)
    {
        _codeRepository = codeRepository;
        _unitOfWork = unitOfWork;
    }

    private async Task<string> GenerateCodeAsync(string prefix, int lastId)
    {
        var newId = lastId + 1;
        return $"{prefix.ToUpper()}-{newId:D6}";
    }

    public async Task<string> GenerateCodeProduct(int categoryId)
    {
        var category = await _codeRepository.GetCategoryByIdAsync(categoryId);
        var prefix = category!.Name.Substring(0, 3);
        var lastCode = await _codeRepository.GetLastCodeByCategoryAsync(categoryId);
        int lastId = ParseLastId(lastCode!);

        return await GenerateCodeAsync(prefix, lastId);
    }

    public async Task<string> GenerateCodeInvoice(int invoiceId)
    {
        var prefix = "INV";
        var lastCode = await _codeRepository.GetLastCodeByInvoiceAsync(invoiceId);
        int lastId = ParseLastId(lastCode!);

        return await GenerateCodeAsync(prefix, lastId);
    }

    public async Task<string> GenerateCodeSale(int saleId)
    {
        var prefix = "SAL";
        var lastCode = await _codeRepository.GetLastCodeBySaleAsync(saleId);
        int lastId = ParseLastId(lastCode!);

        return await GenerateCodeAsync(prefix, lastId);
    }

    public async Task<string> GenerateCodeQuote(int quoteId)
    {
        var prefix = "QTE";
        var lastCode = await _codeRepository.GetLastCodeByQuoteAsync(quoteId);
        int lastId = ParseLastId(lastCode!);

        return await GenerateCodeAsync(prefix, lastId);
    }

    public async Task<string> GenerateSoftwareLicense(int projectId, int licenseTypeId)
    {
        var softwareName = await _unitOfWork.Project.GetByIdAsync(projectId);
        var licenseType = await _unitOfWork.LicenseType.GetByIdAsync(licenseTypeId);

        var prefix = $"{softwareName.InternalName.Substring(0, 3).ToUpper()}-{licenseType.Name.Substring(0, 3).ToUpper()}";
        var uniquePart = GenerateRandomString(16);
        return $"{prefix}-{uniquePart}";
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var randomBytes = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        var stringBuilder = new StringBuilder(length);
        foreach (var byteValue in randomBytes)
        {
            stringBuilder.Append(chars[byteValue % chars.Length]);
        }

        return stringBuilder.ToString();
    }

    private int ParseLastId(string lastCode)
    {
        if (string.IsNullOrEmpty(lastCode) || !lastCode.Contains("-"))
            return 0;

        var parts = lastCode.Split('-');
        return int.TryParse(parts[1], out int lastId) ? lastId : 0;
    }
}
