using POS.Domain.Entities;

namespace POS.Application.Interfaces.Persistence;

public interface IInvoiceDetailRepository
{
    Task<IEnumerable<InvoiceDetail>> GetInvoiceDetailByInvoiceId(int id);
}