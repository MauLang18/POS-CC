using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.PaymentMethod.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.PaymentMethod.Queries.GetAllQuery;

public class GetAllPaymentMethodHandler : IRequestHandler<GetAllPaymentMethodQuery, BaseResponse<IEnumerable<PaymentMethodResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllPaymentMethodHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<PaymentMethodResponseDto>>> Handle(GetAllPaymentMethodQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PaymentMethodResponseDto>>();

        try
        {
            var paymentMethods = _unitOfWork.PaymentMethod.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        paymentMethods = paymentMethods.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                paymentMethods = paymentMethods.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                paymentMethods = paymentMethods.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, paymentMethods)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await paymentMethods.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<PaymentMethodResponseDto>>(items);
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