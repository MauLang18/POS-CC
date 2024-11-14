using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Quote.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Quote.Queries.GetAllQuery;

public class GetAllQuoteHandler : IRequestHandler<GetAllQuoteQuery, BaseResponse<IEnumerable<QuoteResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllQuoteHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<QuoteResponseDto>>> Handle(GetAllQuoteQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<QuoteResponseDto>>();

        try
        {
            var quote = _unitOfWork.Quote.GetAllQueryable()
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
                        quote = quote.Where(x => x.Customer.Name.Contains(request.TextFilter));
                        break;
                    case 2:
                        quote = quote.Where(x => x.VoucherNumber.Contains(request.TextFilter));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                quote = quote.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                           x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, quote)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await quote.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<QuoteResponseDto>>(items);
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