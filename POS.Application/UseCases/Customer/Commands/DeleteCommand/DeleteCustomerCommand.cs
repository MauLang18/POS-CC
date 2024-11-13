using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Customer.Commands.DeleteCommand;

public class DeleteCustomerCommand : IRequest<BaseResponse<bool>>
{
    public int CustomerId { get; set; }
}