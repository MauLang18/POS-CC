using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.PaymentMethod.Response;

namespace POS.Application.UseCases.PaymentMethod.Queries.GetByIdQuery;

public class GetPaymentMethodByIdQuery : IRequest<BaseResponse<PaymentMethodByIdResponseDto>>
{
    public int PaymentMethodId { get; set; }
}