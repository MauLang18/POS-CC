using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Sale.Commands.DeleteCommand;

public class DeleteSaleCommand : IRequest<BaseResponse<bool>>
{
    public int SaleId { get; set; }
}