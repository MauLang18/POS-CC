using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Response;

namespace POS.Application.UseCases.Category.Queries.GetByIdQuery;

public class GetCategoryByIdQuery : IRequest<BaseResponse<CategoryByIdResponseDto>>
{
    public int CategoryId { get; set; }
}