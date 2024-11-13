using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Customer.Response;

namespace POS.Application.UseCases.Customer.Queries.GetByIdQuery;

public class GetCustomerByIdQuery : IRequest<BaseResponse<CustomerByIdResponseDto>>
{
    public int CustomerId { get; set; }
}