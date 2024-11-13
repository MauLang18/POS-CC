using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.CreditType.Response;

namespace POS.Application.UseCases.CreditType.Queries.GetAllQuery;

public class GetAllCreditTypeQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<CreditTypeResponseDto>>>
{
}
