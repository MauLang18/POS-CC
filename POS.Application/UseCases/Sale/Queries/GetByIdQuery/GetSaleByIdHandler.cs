using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Sale.Response;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Sale.Queries.GetByIdQuery;
using POS.Utilities.Static;
using WatchDog;

public class GetSaleByIdHandler : IRequestHandler<GetSaleByIdQuery, BaseResponse<SaleByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSaleByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<SaleByIdResponseDto>> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<SaleByIdResponseDto>();

        try
        {
            var sale = await _unitOfWork.Sale.GetByIdAsync(request.SaleId);

            if (sale == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var customer = await _unitOfWork.Customer.GetByIdAsync(sale.CustomerId);
            if (customer != null)
            {
                sale.Customer = customer;

                var creditType = await _unitOfWork.CreditType.GetByIdAsync(customer.CreditTypeId);
                if (creditType != null)
                {
                    sale.Customer.CreditType = creditType;
                }
            }

            var saleDetails = await _unitOfWork.SaleDetail.GetSaleDetailBySaleId(request.SaleId);
            sale.SaleDetails = saleDetails.ToList();

            sale.AuditCreateDate = sale.AuditCreateDate.Date;

            response.IsSuccess = true;
            response.Data = _mapper.Map<SaleByIdResponseDto>(sale);
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