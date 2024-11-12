using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;

namespace POS.Infrastructure.Persistence.Repositories;

public class QuoteDetailRepository : IQuoteDetailRepository
{
    private readonly ApplicationDbContext _context;

    public QuoteDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<QuoteDetail>> GetQuoteDetailByQuoteId(int id)
    {
        var response = await _context.ProductServices
                .AsNoTracking()
                .Join(_context.QuoteDetails, p => p.Id, pd => pd.ProductServiceId, (p, pd) => new { ProductService = p, QuoteDetail = pd })
                .Where(x => x.QuoteDetail.QuoteId == id)
        .Select(x => new QuoteDetail
        {
            ProductServiceId = x.ProductService.Id,
            ProductService = new ProductService
            {
                Image = x.ProductService.Image,
                Code = x.ProductService.Code,
                Name = x.ProductService.Name,
            },
            Quantity = x.QuoteDetail.Quantity,
            UnitPrice = x.QuoteDetail.UnitPrice,
            Total = x.QuoteDetail.Total
        })
                .ToListAsync();

        return response!;
    }
}