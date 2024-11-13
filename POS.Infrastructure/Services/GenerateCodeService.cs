using POS.Application.Interfaces.Persistence;
using POS.Application.Interfaces.Services;

namespace POS.Infrastructure.Services;

public class GenerateCodeService : IGenerateCodeService
{
    private readonly ICodeRepository _codeRepository; // Repositorio para obtener el último código guardado

    public GenerateCodeService(ICodeRepository codeRepository)
    {
        _codeRepository = codeRepository;
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

    private int ParseLastId(string lastCode)
    {
        if (string.IsNullOrEmpty(lastCode) || !lastCode.Contains("-"))
            return 0;

        var parts = lastCode.Split('-');
        return int.TryParse(parts[1], out int lastId) ? lastId : 0;
    }
}
