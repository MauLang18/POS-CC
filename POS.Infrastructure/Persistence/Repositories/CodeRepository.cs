using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;

namespace POS.Infrastructure.Persistence.Repositories;

public class CodeRepository : ICodeRepository
{
    private readonly ApplicationDbContext _context;

    public CodeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string?> GetLastCodeByCategoryAsync(int categoryId)
    {
        return await _context.ProductServices
            .Where(p => p.CategoryId == categoryId)
            .OrderByDescending(p => p.Code)
            .Select(p => p.Code)
            .FirstOrDefaultAsync();
    }

    public async Task<string?> GetLastCodeByInvoiceAsync(int invoiceId)
    {
        return await _context.Invoices
            .OrderByDescending(i => i.VoucherNumber)
            .Select(i => i.VoucherNumber)
            .FirstOrDefaultAsync();
    }

    public async Task<string?> GetLastCodeBySaleAsync(int saleId)
    {
        return await _context.Sales
            .OrderByDescending(s => s.VoucherNumber)
            .Select(s => s.VoucherNumber)
            .FirstOrDefaultAsync();
    }

    public async Task<string?> GetLastCodeByQuoteAsync(int quoteId)
    {
        return await _context.Quotes
            .OrderByDescending(q => q.VoucherNumber)
            .Select(q => q.VoucherNumber)
            .FirstOrDefaultAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId);
    }
}