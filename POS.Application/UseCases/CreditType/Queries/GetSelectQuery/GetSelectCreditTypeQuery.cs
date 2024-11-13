using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.CreditType.Queries.GetSelectQuery;

public class GetSelectCreditTypeQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}