using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Project.Response;

namespace POS.Application.UseCases.Project.Queries.GetByIdQuery;

public class GetProjectByIdQuery : IRequest<BaseResponse<ProjectByIdResponseDto>>
{
    public int ProjectId { get; set; }
}