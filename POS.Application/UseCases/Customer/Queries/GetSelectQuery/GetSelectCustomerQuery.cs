using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.Customer.Queries.GetSelectQuery;

public class GetSelectCustomerQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}