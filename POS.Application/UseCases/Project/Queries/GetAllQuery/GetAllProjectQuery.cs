using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Project.Response;

namespace POS.Application.UseCases.Project.Queries.GetAllQuery;

public class GetAllProjectQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<ProjectResponseDto>>>
{
}