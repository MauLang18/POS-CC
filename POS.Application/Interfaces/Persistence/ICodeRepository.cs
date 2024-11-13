using POS.Domain.Entities;

namespace POS.Application.Interfaces.Persistence;

public interface ICodeRepository
{
    Task<string?> GetLastCodeByCategoryAsync(int categoryId);
    Task<string?> GetLastCodeByInvoiceAsync(int invoiceId);
    Task<string?> GetLastCodeBySaleAsync(int saleId);
    Task<string?> GetLastCodeByQuoteAsync(int quoteId);
    Task<Category?> GetCategoryByIdAsync(int categoryId);
}