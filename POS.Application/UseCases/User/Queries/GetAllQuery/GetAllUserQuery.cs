using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.User.Response;

namespace POS.Application.UseCases.User.Queries.GetAllQuery;

public class GetAllUserQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<UserResponseDto>>>
{
}