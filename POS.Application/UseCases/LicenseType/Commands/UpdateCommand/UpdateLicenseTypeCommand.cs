using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.LicenseType.Commands.UpdateCommand;

public class UpdateLicenseTypeCommand : IRequest<BaseResponse<bool>>
{
    public int LicenseTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}