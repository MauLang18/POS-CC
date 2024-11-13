using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Unit.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Unit.Queries.GetByIdQuery;

public class GetUnitByIdHandler : IRequestHandler<GetUnitByIdQuery, BaseResponse<UnitByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUnitByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<UnitByIdResponseDto>> Handle(GetUnitByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UnitByIdResponseDto>();

        try
        {
            var unit = await _unitOfWork.Unit.GetByIdAsync(request.UnitId);

            if (unit is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<UnitByIdResponseDto>(unit);
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