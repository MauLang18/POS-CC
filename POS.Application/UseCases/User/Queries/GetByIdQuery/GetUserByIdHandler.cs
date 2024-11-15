using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.User.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.User.Queries.GetByIdQuery;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, BaseResponse<UserByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<UserByIdResponseDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<UserByIdResponseDto>();

        try
        {
            var user = await _unitOfWork.User.GetByIdAsync(request.UserId);

            if (user is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<UserByIdResponseDto>(user);
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