using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Purchase.Commands.DeleteCommand;

public class DeletePurchaseCommand : IRequest<BaseResponse<bool>>
{
    public int PurchaseId { get; set; }
}