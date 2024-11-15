using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Supplier.Response;

namespace POS.Application.UseCases.Supplier.Queries.GetByIdQuery;

public class GetSupplierByIdQuery : IRequest<BaseResponse<SupplierByIdResponseDto>>
{
    public int SupplierId { get; set; }
}