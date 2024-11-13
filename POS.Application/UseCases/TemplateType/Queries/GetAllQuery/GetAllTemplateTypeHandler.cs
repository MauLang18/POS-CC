using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.TemplateType.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.TemplateType.Queries.GetAllQuery;

public class GetAllTemplateTypeHandler : IRequestHandler<GetAllTemplateTypeQuery, BaseResponse<IEnumerable<TemplateTypeResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllTemplateTypeHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }
    public async Task<BaseResponse<IEnumerable<TemplateTypeResponseDto>>> Handle(GetAllTemplateTypeQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<TemplateTypeResponseDto>>();

        try
        {
            var templateTypes = _unitOfWork.TemplateType.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        templateTypes = templateTypes.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                templateTypes = templateTypes.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                templateTypes = templateTypes.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, templateTypes)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await templateTypes.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<TemplateTypeResponseDto>>(items);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}