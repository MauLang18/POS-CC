using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.License.Commands.UpdateCommand;

public class UpdateLicenseCommand : IRequest<BaseResponse<bool>>
{
    public int LicenseId { get; set; }
    public int ProjectId { get; set; }
    public int LicenseTypeId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int State { get; set; }
}