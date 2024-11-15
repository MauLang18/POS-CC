using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.LicenseType.Response;

namespace POS.Application.UseCases.LicenseType.Queries.GetByIdQuery;

public class GetLicenseTypeByIdQuery : IRequest<BaseResponse<LicenseTypeByIdResponseDto>>
{
    public int LicenseTypeId { get; set; }
}