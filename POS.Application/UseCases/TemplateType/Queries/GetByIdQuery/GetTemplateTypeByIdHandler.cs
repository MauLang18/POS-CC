using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.TemplateType.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.TemplateType.Queries.GetByIdQuery;

public class GetTemplateTypeByIdHandler : IRequestHandler<GetTemplateTypeByIdQuery, BaseResponse<TemplateTypeByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTemplateTypeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<TemplateTypeByIdResponseDto>> Handle(GetTemplateTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<TemplateTypeByIdResponseDto>();

        try
        {
            var templateType = await _unitOfWork.TemplateType.GetByIdAsync(request.TemplateTypeId);

            if (templateType is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<TemplateTypeByIdResponseDto>(templateType);
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