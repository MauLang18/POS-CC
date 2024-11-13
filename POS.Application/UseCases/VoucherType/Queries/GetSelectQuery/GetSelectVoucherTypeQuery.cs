using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.VoucherType.Queries.GetSelectQuery;

public class GetSelectVoucherTypeQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}