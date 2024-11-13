using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.CreditType.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.CreditType.Queries.GetByIdQuery;

public class GetCreditTypeByIdHandler : IRequestHandler<GetCreditTypeByIdQuery, BaseResponse<CreditTypeByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCreditTypeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<CreditTypeByIdResponseDto>> Handle(GetCreditTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CreditTypeByIdResponseDto>();

        try
        {
            var creditType = await _unitOfWork.CreditType.GetByIdAsync(request.CreditTypeId);

            if (creditType is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<CreditTypeByIdResponseDto>(creditType);
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