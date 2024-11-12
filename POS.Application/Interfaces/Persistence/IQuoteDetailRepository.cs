using POS.Domain.Entities;

namespace POS.Application.Interfaces.Persistence;

public interface IQuoteDetailRepository
{
    Task<IEnumerable<QuoteDetail>> GetQuoteDetailByQuoteId(int id);
}