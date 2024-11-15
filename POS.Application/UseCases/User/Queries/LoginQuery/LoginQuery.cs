using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.User.Queries.LoginQuery;

public class LoginQuery : IRequest<BaseResponse<string>>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string PassWord { get; set; } = null!;
}