using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Sale.Commands.CreateCommand;

public class CreateSaleCommand : IRequest<BaseResponse<bool>>
{
    public int VoucherTypeId { get; set; }
    public int CustomerId { get; set; }
    public int? QuoteId { get; set; }
    public int? ProjectId { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public int StatusId { get; set; }
    public IEnumerable<CreateSaleDetailCommand> SaleDetails { get; set; } = null!;
}

public class CreateSaleDetailCommand
{
    public int ProductServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}