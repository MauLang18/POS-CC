using POS.Domain.Entities;

namespace POS.Application.Interfaces.Persistence;

public interface ISaleDetailRepository
{
    Task<IEnumerable<SaleDetail>> GetSaleDetailBySaleId(int id);
}