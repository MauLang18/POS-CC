using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.ProductService.Response;

namespace POS.Application.UseCases.ProductService.Queries.GetAllQuery;

public class GetAllProductServiceQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<ProductServiceResponseDto>>>
{
}