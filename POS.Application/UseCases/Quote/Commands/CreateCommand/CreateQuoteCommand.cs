using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Quote.Commands.CreateCommand;

public class CreateQuoteCommand : IRequest<BaseResponse<bool>>
{
    public int VoucherTypeId { get; set; }
    public int CustomerId { get; set; }
    public int PaymentMethodId { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public int StatusId { get; set; }
    public IEnumerable<CreateQuoteDetailCommand> QuoteDetails { get; set; } = null!;
}

public class CreateQuoteDetailCommand
{
    public int ProductServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
}