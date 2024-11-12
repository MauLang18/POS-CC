using POS.Domain.Entities;

namespace POS.Application.Interfaces.Persistence;

public interface IPurchaseDetailRepository
{
    Task<IEnumerable<PurchaseDetail>> GetPurchaseDetailByPurchaseId(int id);
}