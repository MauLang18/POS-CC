using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.VoucherType.Response;

namespace POS.Application.UseCases.VoucherType.Queries.GetByIdQuery;

public class GetVoucherTypeByIdQuery : IRequest<BaseResponse<VoucherTypeByIdResponseDto>>
{
    public int VoucherTypeId { get; set; }
}