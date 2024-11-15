using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.LicenseType.Commands.DeleteCommand;

public class DeleteLicenseTypeCommand : IRequest<BaseResponse<bool>>
{
    public int LicenseTypeId { get; set; }
}