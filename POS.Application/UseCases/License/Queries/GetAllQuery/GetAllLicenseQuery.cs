using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.License.Response;

namespace POS.Application.UseCases.License.Queries.GetAllQuery;

public class GetAllLicenseQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<LicenseResponseDto>>>
{
}