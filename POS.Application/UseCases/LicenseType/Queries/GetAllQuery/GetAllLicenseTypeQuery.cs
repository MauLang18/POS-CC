using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.LicenseType.Response;

namespace POS.Application.UseCases.LicenseType.Queries.GetAllQuery;

public class GetAllLicenseTypeQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<LicenseTypeResponseDto>>>
{
}