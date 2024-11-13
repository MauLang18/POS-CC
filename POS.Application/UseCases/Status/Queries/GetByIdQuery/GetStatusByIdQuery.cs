using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Status.Response;

namespace POS.Application.UseCases.Status.Queries.GetByIdQuery;

public class GetStatusByIdQuery : IRequest<BaseResponse<StatusByIdResponseDto>>
{
    public int StatusId { get; set; }
}