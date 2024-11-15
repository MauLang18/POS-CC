using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.Sale.Queries.GetSelectQuery;

public class GetSelectSaleQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}