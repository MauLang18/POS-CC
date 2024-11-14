using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.PaymentMethod.Response;

namespace POS.Application.UseCases.PaymentMethod.Queries.GetAllQuery;

public class GetAllPaymentMethodQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<PaymentMethodResponseDto>>>
{
}