using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.ProductService.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.ProductService.Queries.GetAllQuery;

public class GetAllProductServiceHandler : IRequestHandler<GetAllProductServiceQuery, BaseResponse<IEnumerable<ProductServiceResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllProductServiceHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<ProductServiceResponseDto>>> Handle(GetAllProductServiceQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProductServiceResponseDto>>();

        try
        {
            var productServices = _unitOfWork.ProductService.GetAllQueryable()
                .Include(x => x.Category)
                .Include(x => x.Unit)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        productServices = productServices.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                    case 2:
                        productServices = productServices.Where(x => x.Code.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                productServices = productServices.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                productServices = productServices.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, productServices)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await productServices.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<ProductServiceResponseDto>>(items);
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