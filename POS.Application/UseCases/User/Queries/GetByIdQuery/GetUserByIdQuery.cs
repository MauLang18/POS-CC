using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.User.Response;

namespace POS.Application.UseCases.User.Queries.GetByIdQuery;

public class GetUserByIdQuery : IRequest<BaseResponse<UserByIdResponseDto>>
{
    public int UserId { get; set; }
}