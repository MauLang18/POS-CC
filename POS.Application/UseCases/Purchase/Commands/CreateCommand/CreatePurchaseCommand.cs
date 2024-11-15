using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Purchase.Commands.CreateCommand;

public class CreatePurchaseCommand : IRequest<BaseResponse<bool>>
{
    public int SupplierId { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public IEnumerable<CreatePurchaseDetailCommand> PurchaseDetails { get; set; } = null!;
}

public class CreatePurchaseDetailCommand
{
    public int ProductServiceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total { get; set; }
}