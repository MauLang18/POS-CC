using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Supplier.Commands.DeleteCommand;

public class DeleteSupplierCommand : IRequest<BaseResponse<bool>>
{
    public int SupplierId { get; set; }
}