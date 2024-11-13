using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Status.Response;

namespace POS.Application.UseCases.Status.Queries.GetAllQuery;

public class GetAllStatusQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<StatusResponseDto>>>
{
}