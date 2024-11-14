using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.EmailTemplate.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.EmailTemplate.Queries.GetAllQuery;

public class GetAllEmailTemplateHandler : IRequestHandler<GetAllEmailTemplateQuery, BaseResponse<IEnumerable<EmailTemplateResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllEmailTemplateHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<EmailTemplateResponseDto>>> Handle(GetAllEmailTemplateQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<EmailTemplateResponseDto>>();

        try
        {
            var emailTemplates = _unitOfWork.EmailTemplate.GetAllQueryable()
                .Include(x => x.TemplateType)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        emailTemplates = emailTemplates.Where(x => x.Subject.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                emailTemplates = emailTemplates.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                emailTemplates = emailTemplates.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, emailTemplates)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await emailTemplates.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<EmailTemplateResponseDto>>(items);
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