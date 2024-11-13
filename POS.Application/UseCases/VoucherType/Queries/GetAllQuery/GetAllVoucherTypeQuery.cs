using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.VoucherType.Response;

namespace POS.Application.UseCases.VoucherType.Queries.GetAllQuery;

public class GetAllVoucherTypeQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<VoucherTypeResponseDto>>>
{
}