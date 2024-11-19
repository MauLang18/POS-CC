using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;

namespace POS.Infrastructure.Persistence.Repositories;

public class SaleDetailRepository : ISaleDetailRepository
{
    private readonly ApplicationDbContext _context;

    public SaleDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SaleDetail>> GetSaleDetailBySaleId(int id)
    {
        var response = await _context.ProductServices
                .AsNoTracking()
                .Join(_context.SaleDetails, p => p.Id, pd => pd.ProductServiceId, (p, pd) => new { ProductService = p, SaleDetail = pd })
                .Where(x => x.SaleDetail.SaleId == id)
        .Select(x => new SaleDetail
        {
            ProductServiceId = x.ProductService.Id,
            ProductService = new ProductService
            {
                Image = x.ProductService.Image,
                Code = x.ProductService.Code,
                Name = x.ProductService.Name,
            },
            Quantity = x.SaleDetail.Quantity,
            Price = x.SaleDetail.Price,
            Total = x.SaleDetail.Total
        })
                .ToListAsync();

        return response!;
    }
}