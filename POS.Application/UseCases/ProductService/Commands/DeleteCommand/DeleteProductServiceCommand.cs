using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.ProductService.Commands.DeleteCommand;

public class DeleteProductServiceCommand : IRequest<BaseResponse<bool>>
{
    public int ProductServiceId { get; set; }
}