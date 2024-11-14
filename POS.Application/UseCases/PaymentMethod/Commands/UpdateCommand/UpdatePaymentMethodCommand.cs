using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.PaymentMethod.Commands.UpdateCommand;

public class UpdatePaymentMethodCommand : IRequest<BaseResponse<bool>>
{
    public int PaymentMethodId { get; set; }
    public string Name { get; set; } = null!;
    public int State { get; set; }
}