using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.PaymentMethod.Queries.GetSelectQuery;

public class GetSelectPaymentMethodQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}