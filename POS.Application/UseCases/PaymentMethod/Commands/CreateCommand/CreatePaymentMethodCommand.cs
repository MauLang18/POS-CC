using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.PaymentMethod.Commands.CreateCommand;

public class CreatePaymentMethodCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public int State { get; set; }
}