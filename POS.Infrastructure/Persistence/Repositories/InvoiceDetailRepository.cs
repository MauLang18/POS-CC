using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Persistence;
using POS.Domain.Entities;
using POS.Infrastructure.Persistence.Contexts;

namespace POS.Infrastructure.Persistence.Repositories;

public class InvoiceDetailRepository : IInvoiceDetailRepository
{
    private readonly ApplicationDbContext _context;

    public InvoiceDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InvoiceDetail>> GetInvoiceDetailByInvoiceId(int id)
    {
        var response = await _context.ProductServices
                .AsNoTracking()
                .Join(_context.InvoicesDetails, p => p.Id, pd => pd.ProductServiceId, (p, pd) => new { ProductService = p, InvoiceDetail = pd })
                .Where(x => x.InvoiceDetail.InvoiceId == id)
        .Select(x => new InvoiceDetail
        {
            ProductServiceId = x.ProductService.Id,
            ProductService = new ProductService
            {
                Image = x.ProductService.Image,
                Code = x.ProductService.Code,
                Name = x.ProductService.Name,
            },
            Quantity = x.InvoiceDetail.Quantity,
            UnitPrice = x.InvoiceDetail.UnitPrice,
            Total = x.InvoiceDetail.Total
        })
                .ToListAsync();

        return response!;
    }
}