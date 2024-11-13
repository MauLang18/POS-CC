using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.DocumentTemplate.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.DocumentTemplate.Queries.GetAllQuery;

public class GetAllDocumentTemplateHandler : IRequestHandler<GetAllDocumentTemplateQuery, BaseResponse<IEnumerable<DocumentTemplateResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllDocumentTemplateHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<DocumentTemplateResponseDto>>> Handle(GetAllDocumentTemplateQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<DocumentTemplateResponseDto>>();

        try
        {
            var documentTemplates = _unitOfWork.DocumentTemplate.GetAllQueryable()
                .Include(x => x.TemplateType)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        documentTemplates = documentTemplates.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                documentTemplates = documentTemplates.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                documentTemplates = documentTemplates.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, documentTemplates)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await documentTemplates.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<DocumentTemplateResponseDto>>(items);
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