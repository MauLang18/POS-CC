using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Status.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Status.Queries.GetByIdQuery;

public class GetStatusByIdHandler : IRequestHandler<GetStatusByIdQuery, BaseResponse<StatusByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetStatusByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<StatusByIdResponseDto>> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<StatusByIdResponseDto>();

        try
        {
            var status = await _unitOfWork.Status.GetByIdAsync(request.StatusId);

            if (status is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<StatusByIdResponseDto>(status);
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