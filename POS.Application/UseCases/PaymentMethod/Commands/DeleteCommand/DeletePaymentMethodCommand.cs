using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.PaymentMethod.Commands.DeleteCommand;

public class DeletePaymentMethodCommand : IRequest<BaseResponse<bool>>
{
    public int PaymentMethodId { get; set; }
}