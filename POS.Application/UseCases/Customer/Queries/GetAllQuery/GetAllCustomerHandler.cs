using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Customer.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Customer.Queries.GetAllQuery;

public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, BaseResponse<IEnumerable<CustomerResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<CustomerResponseDto>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<CustomerResponseDto>>();

        try
        {
            var customers = _unitOfWork.Customer.GetAllQueryable()
                .Include(x => x.DocumentType)
                .Include(x => x.CreditType)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        customers = customers.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                    case 2:
                        customers = customers.Where(x => x.Email!.Contains(request.TextFilter));
                        break;
                    case 3:
                        customers = customers.Where(x => x.DocumentNumber.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                customers = customers.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                customers = customers.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, customers)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await customers.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<CustomerResponseDto>>(items);
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