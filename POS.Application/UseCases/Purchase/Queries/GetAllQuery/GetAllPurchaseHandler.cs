using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Purchase.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Purchase.Queries.GetAllQuery;

public class GetAllPurchaseHandler : IRequestHandler<GetAllPurchaseQuery, BaseResponse<IEnumerable<PurchaseResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllPurchaseHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<PurchaseResponseDto>>> Handle(GetAllPurchaseQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<PurchaseResponseDto>>();

        try
        {
            var purchase = _unitOfWork.Purchase.GetAllQueryable()
                .AsNoTracking()
                .Include(x => x.Supplier)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        purchase = purchase.Where(x => x.Supplier.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                purchase = purchase.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                           x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, purchase)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await purchase.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<PurchaseResponseDto>>(items);
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