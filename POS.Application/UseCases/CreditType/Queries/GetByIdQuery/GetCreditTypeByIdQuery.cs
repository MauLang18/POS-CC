using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.CreditType.Response;

namespace POS.Application.UseCases.CreditType.Queries.GetByIdQuery;

public class GetCreditTypeByIdQuery : IRequest<BaseResponse<CreditTypeByIdResponseDto>>
{
    public int CreditTypeId { get; set; }
}