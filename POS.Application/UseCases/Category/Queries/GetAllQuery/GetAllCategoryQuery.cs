using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Response;

namespace POS.Application.UseCases.Category.Queries.GetAllQuery;

public class GetAllCategoryQuery : BaseFilters, IRequest<BaseResponse<IEnumerable<CategoryResponseDto>>>
{
}