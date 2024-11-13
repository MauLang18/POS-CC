using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Customer.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Customer.Queries.GetByIdQuery;

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, BaseResponse<CustomerByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCustomerByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<CustomerByIdResponseDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CustomerByIdResponseDto>();

        try
        {
            var customer = await _unitOfWork.Customer.GetByIdAsync(request.CustomerId);

            if (customer is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<CustomerByIdResponseDto>(customer);
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
