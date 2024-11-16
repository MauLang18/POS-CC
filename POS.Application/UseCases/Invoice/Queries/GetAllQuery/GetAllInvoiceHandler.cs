using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Invoice.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Invoice.Queries.GetAllQuery;

public class GetAllInvoiceHandler : IRequestHandler<GetAllInvoiceQuery, BaseResponse<IEnumerable<InvoiceResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllInvoiceHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<InvoiceResponseDto>>> Handle(GetAllInvoiceQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<InvoiceResponseDto>>();

        try
        {
            var invoice = _unitOfWork.Invoice.GetAllQueryable()
                .AsNoTracking()
                .Include(x => x.Sale)
                .Include(x => x.Status)
                .Include(x => x.VoucherType)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        invoice = invoice.Where(x => x.Sale.VoucherNumber.Contains(request.TextFilter));
                        break;
                    case 2:
                        invoice = invoice.Where(x => x.VoucherNumber.Contains(request.TextFilter));
                        break;
                }
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                invoice = invoice.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                           x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, invoice)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await invoice.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<InvoiceResponseDto>>(items);
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