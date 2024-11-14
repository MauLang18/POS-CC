using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.EmailTemplate.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.EmailTemplate.Queries.GetByIdQuery;

public class GetEmailTemplateByIdHandler : IRequestHandler<GetEmailTemplateByIdQuery, BaseResponse<EmailTemplateByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetEmailTemplateByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<EmailTemplateByIdResponseDto>> Handle(GetEmailTemplateByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<EmailTemplateByIdResponseDto>();

        try
        {
            var emailTemplate = await _unitOfWork.EmailTemplate.GetByIdAsync(request.EmailTemplateId);

            if (emailTemplate is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<EmailTemplateByIdResponseDto>(emailTemplate);
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