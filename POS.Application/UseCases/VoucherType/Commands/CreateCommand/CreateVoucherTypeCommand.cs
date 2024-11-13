using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.VoucherType.Commands.CreateCommand;

public class CreateVoucherTypeCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;
    public int State { get; set; }
}