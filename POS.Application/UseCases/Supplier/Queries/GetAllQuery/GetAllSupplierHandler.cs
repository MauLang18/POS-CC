using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Supplier.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Supplier.Queries.GetAllQuery;

public class GetAllSupplierHandler : IRequestHandler<GetAllSupplierQuery, BaseResponse<IEnumerable<SupplierResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllSupplierHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<SupplierResponseDto>>> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SupplierResponseDto>>();

        try
        {
            var suppliers = _unitOfWork.Supplier.GetAllQueryable()
                .Include(x => x.DocumentType)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        suppliers = suppliers.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                    case 2:
                        suppliers = suppliers.Where(x => x.Email!.Contains(request.TextFilter));
                        break;
                    case 3:
                        suppliers = suppliers.Where(x => x.DocumentNumber.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                suppliers = suppliers.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                suppliers = suppliers.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, suppliers)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await suppliers.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<SupplierResponseDto>>(items);
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