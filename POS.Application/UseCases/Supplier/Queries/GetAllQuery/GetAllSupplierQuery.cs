using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Supplier.Response;

namespace POS.Application.UseCases.Supplier.Queries.GetAllQuery;

public class GetAllSupplierQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<SupplierResponseDto>>>
{
}