using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.LicenseType.Commands.CreateCommand;

public class CreateLicenseTypeCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int State { get; set; }
}