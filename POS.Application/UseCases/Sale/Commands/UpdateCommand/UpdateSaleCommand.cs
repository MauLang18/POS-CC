using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Sale.Commands.UpdateCommand;

public class UpdateSaleCommand : IRequest<BaseResponse<bool>>
{
    public int SaleId { get; set; }
    public int StatusId { get; set; }
}