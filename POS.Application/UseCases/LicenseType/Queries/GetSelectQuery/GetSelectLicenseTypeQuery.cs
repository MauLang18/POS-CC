using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.LicenseType.Queries.GetSelectQuery;

public class GetSelectLicenseTypeQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}