using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.VoucherType.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.VoucherType.Queries.GetByIdQuery;

public class GetVoucherTypeByIdHandler : IRequestHandler<GetVoucherTypeByIdQuery, BaseResponse<VoucherTypeByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetVoucherTypeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<VoucherTypeByIdResponseDto>> Handle(GetVoucherTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<VoucherTypeByIdResponseDto>();

        try
        {
            var voucherType = await _unitOfWork.VoucherType.GetByIdAsync(request.VoucherTypeId);

            if (voucherType is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<VoucherTypeByIdResponseDto>(voucherType);
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