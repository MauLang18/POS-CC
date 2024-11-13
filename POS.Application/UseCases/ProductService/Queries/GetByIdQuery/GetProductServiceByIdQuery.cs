using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.ProductService.Response;

namespace POS.Application.UseCases.ProductService.Queries.GetByIdQuery;

public class GetProductServiceByIdQuery : IRequest<BaseResponse<ProductServiceByIdResponseDto>>
{
    public int ProductServiceId { get; set; }
}