using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Sale.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Sale.Queries.GetAllQuery;

public class GetAllSaleHandler : IRequestHandler<GetAllSaleQuery, BaseResponse<IEnumerable<SaleResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllSaleHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<SaleResponseDto>>> Handle(GetAllSaleQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SaleResponseDto>>();

        try
        {
            var sale = _unitOfWork.Sale.GetAllQueryable()
                .AsNoTracking()
                .Include(x => x.Customer)
                .Include(x => x.Status)
                .Include(x => x.VoucherType)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        sale = sale.Where(x => x.Customer.Name.Contains(request.TextFilter));
                        break;
                    case 2:
                        sale = sale.Where(x => x.VoucherNumber.Contains(request.TextFilter));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                sale = sale.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                           x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, sale)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await sale.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<SaleResponseDto>>(items);
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