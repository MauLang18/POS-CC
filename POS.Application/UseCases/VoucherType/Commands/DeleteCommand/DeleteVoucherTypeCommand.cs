using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.VoucherType.Commands.DeleteCommand;

public class DeleteVoucherTypeCommand : IRequest<BaseResponse<bool>>
{
    public int VoucherTypeId { get; set; }
}