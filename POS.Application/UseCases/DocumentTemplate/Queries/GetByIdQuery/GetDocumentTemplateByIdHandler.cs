using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.DocumentTemplate.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.DocumentTemplate.Queries.GetByIdQuery;

public class GetDocumentTemplateByIdHandler : IRequestHandler<GetDocumentTemplateByIdQuery, BaseResponse<DocumentTemplateByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDocumentTemplateByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<DocumentTemplateByIdResponseDto>> Handle(GetDocumentTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<DocumentTemplateByIdResponseDto>();

        try
        {
            var documentTemplate = await _unitOfWork.DocumentTemplate.GetByIdAsync(request.DocumentTemplateId);

            if (documentTemplate is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<DocumentTemplateByIdResponseDto>(documentTemplate);
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