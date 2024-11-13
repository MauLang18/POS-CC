using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.CreditType.Commands.DeleteCommand;

public class DeleteCreditTypeCommand : IRequest<BaseResponse<bool>>
{
    public int CreditTypeId { get; set; }
}