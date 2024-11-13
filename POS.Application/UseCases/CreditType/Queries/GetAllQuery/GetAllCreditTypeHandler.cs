using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.CreditType.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.CreditType.Queries.GetAllQuery;

public class GetAllCreditTypeHandler : IRequestHandler<GetAllCreditTypeQuery, BaseResponse<IEnumerable<CreditTypeResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllCreditTypeHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<CreditTypeResponseDto>>> Handle(GetAllCreditTypeQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<CreditTypeResponseDto>>();

        try
        {
            var creditTypes = _unitOfWork.CreditType.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        creditTypes = creditTypes.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                creditTypes = creditTypes.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                creditTypes = creditTypes.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, creditTypes)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await creditTypes.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<CreditTypeResponseDto>>(items);
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