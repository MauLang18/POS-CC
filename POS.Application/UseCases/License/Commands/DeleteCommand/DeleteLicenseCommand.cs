using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.License.Commands.DeleteCommand;

public class DeleteLicenseCommand : IRequest<BaseResponse<bool>>
{
    public int LicenseId { get; set; }
}