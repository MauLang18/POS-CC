using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;

namespace POS.Application.UseCases.Supplier.Queries.GetSelectQuery;

public class GetSelectSupplierQuery : IRequest<BaseResponse<IEnumerable<SelectResponse>>>
{
}