using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Purchase.Response;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Purchase.Queries.GetByIdQuery;
using POS.Utilities.Static;
using WatchDog;

public class GetPurchaseByIdHandler : IRequestHandler<GetPurchaseByIdQuery, BaseResponse<PurchaseByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPurchaseByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<PurchaseByIdResponseDto>> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<PurchaseByIdResponseDto>();

        try
        {
            var purchase = await _unitOfWork.Purchase.GetByIdAsync(request.PurchaseId);

            if (purchase == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var purchaseDetails = await _unitOfWork.PurchaseDetail.GetPurchaseDetailByPurchaseId(request.PurchaseId);
            purchase.PurchaseDetails = purchaseDetails.ToList();

            purchase.AuditCreateDate = purchase.AuditCreateDate.Date;

            response.IsSuccess = true;
            response.Data = _mapper.Map<PurchaseByIdResponseDto>(purchase);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}