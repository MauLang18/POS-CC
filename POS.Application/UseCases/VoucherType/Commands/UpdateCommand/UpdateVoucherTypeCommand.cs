using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.VoucherType.Commands.UpdateCommand;

public class UpdateVoucherTypeCommand : IRequest<BaseResponse<bool>>
{
    public int VoucherTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Abbreviation { get; set; }
    public int State { get; set; }
}