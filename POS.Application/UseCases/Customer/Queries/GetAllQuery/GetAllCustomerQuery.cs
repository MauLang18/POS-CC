using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Customer.Response;

namespace POS.Application.UseCases.Customer.Queries.GetAllQuery;

public class GetAllCustomerQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<CustomerResponseDto>>>
{
}