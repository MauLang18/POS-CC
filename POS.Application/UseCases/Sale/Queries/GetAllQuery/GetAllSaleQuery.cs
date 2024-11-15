using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Sale.Response;

namespace POS.Application.UseCases.Sale.Queries.GetAllQuery;

public class GetAllSaleQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<SaleResponseDto>>>
{
}