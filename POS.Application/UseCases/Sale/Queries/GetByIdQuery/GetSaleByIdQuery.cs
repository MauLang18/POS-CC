using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Sale.Response;

namespace POS.Application.UseCases.Sale.Queries.GetByIdQuery;

public class GetSaleByIdQuery : IRequest<BaseResponse<SaleByIdResponseDto>>
{
    public int SaleId { get; set; }
}