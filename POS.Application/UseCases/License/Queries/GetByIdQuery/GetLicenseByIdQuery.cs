using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.License.Response;

namespace POS.Application.UseCases.License.Queries.GetByIdQuery;

public class GetLicenseByIdQuery : IRequest<BaseResponse<LicenseByIdResponseDto>>
{
    public int LicenseId { get; set; }
}