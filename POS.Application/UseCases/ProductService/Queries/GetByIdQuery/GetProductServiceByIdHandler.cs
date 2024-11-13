using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.ProductService.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.ProductService.Queries.GetByIdQuery;

public class GetProductServiceByIdHandler : IRequestHandler<GetProductServiceByIdQuery, BaseResponse<ProductServiceByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductServiceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProductServiceByIdResponseDto>> Handle(GetProductServiceByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ProductServiceByIdResponseDto>();

        try
        {
            var productService = await _unitOfWork.ProductService.GetByIdAsync(request.ProductServiceId);

            if (productService is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<ProductServiceByIdResponseDto>(productService);
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