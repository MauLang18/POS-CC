using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.PaymentMethod.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.PaymentMethod.Queries.GetByIdQuery;

public class GetPaymentMethodByIdHandler : IRequestHandler<GetPaymentMethodByIdQuery, BaseResponse<PaymentMethodByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaymentMethodByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<PaymentMethodByIdResponseDto>> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<PaymentMethodByIdResponseDto>();

        try
        {
            var paymentMethod = await _unitOfWork.PaymentMethod.GetByIdAsync(request.PaymentMethodId);

            if (paymentMethod is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<PaymentMethodByIdResponseDto>(paymentMethod);
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