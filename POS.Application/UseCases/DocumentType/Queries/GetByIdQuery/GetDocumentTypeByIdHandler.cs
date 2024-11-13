using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.DocumentType.Response;
using POS.Application.Dtos.DocumentType.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.DocumentType.Queries.GetByIdQuery;

public class GetDocumentTypeByIdHandler : IRequestHandler<GetDocumentTypeByIdQuery, BaseResponse<DocumentTypeByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetDocumentTypeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<DocumentTypeByIdResponseDto>> Handle(GetDocumentTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<DocumentTypeByIdResponseDto>();

        try
        {
            var documentType = await _unitOfWork.DocumentType.GetByIdAsync(request.DocumentTypeId);

            if (documentType is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<DocumentTypeByIdResponseDto>(documentType);
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