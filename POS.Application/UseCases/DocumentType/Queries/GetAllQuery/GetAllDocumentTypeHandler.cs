using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.DocumentType.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.DocumentType.Queries.GetAllQuery;

public class GetAllDocumentTypeHandler : IRequestHandler<GetAllDocumentTypeQuery, BaseResponse<IEnumerable<DocumentTypeResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllDocumentTypeHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<DocumentTypeResponseDto>>> Handle(GetAllDocumentTypeQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<DocumentTypeResponseDto>>();

        try
        {
            var documentTypes = _unitOfWork.DocumentType.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        documentTypes = documentTypes.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                documentTypes = documentTypes.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                documentTypes = documentTypes.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, documentTypes)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await documentTypes.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<DocumentTypeResponseDto>>(items);
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