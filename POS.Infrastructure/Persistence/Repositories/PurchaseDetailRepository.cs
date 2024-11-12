using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;

namespace POS.Infrastructure.Persistence.Repositories;

public class PurchaseDetailRepository : IPurchaseDetailRepository
{
    private readonly ApplicationDbContext _context;

    public PurchaseDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PurchaseDetail>> GetPurchaseDetailByPurchaseId(int id)
    {
        var response = await _context.ProductServices
                .AsNoTracking()
                .Join(_context.PurchaseDetails, p => p.Id, pd => pd.ProductServiceId, (p, pd) => new { ProductService = p, PurchaseDetail = pd })
                .Where(x => x.PurchaseDetail.PurchaseId == id)
        .Select(x => new PurchaseDetail
        {
            ProductServiceId = x.ProductService.Id,
            ProductService = new ProductService
            {
                Image = x.ProductService.Image,
                Code = x.ProductService.Code,
                Name = x.ProductService.Name,
            },
            Quatity = x.PurchaseDetail.Quatity,
            UnitPrice = x.PurchaseDetail.UnitPrice,
            Total = x.PurchaseDetail.Total
        })
                .ToListAsync();

        return response!;
    }
}