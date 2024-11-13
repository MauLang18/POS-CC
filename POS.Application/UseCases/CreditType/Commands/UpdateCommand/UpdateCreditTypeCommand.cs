using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.CreditType.Commands.UpdateCommand;

public class UpdateCreditTypeCommand : IRequest<BaseResponse<bool>>
{
    public int CreditTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}