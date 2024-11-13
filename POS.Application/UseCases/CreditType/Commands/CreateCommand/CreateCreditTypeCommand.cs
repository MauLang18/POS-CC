using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.CreditType.Commands.CreateCommand;

public class CreateCreditTypeCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}