using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using BC = BCrypt.Net.BCrypt;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.User.Commands.UpdateCommand;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existUser = await _unitOfWork.User.GetByIdAsync(request.UserId);

            if (existUser is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var user = _mapper.Map<Entity.User>(request);
            user.Id = request.UserId;

            if (request.Password is not null)
                user.Password = BC.HashPassword(request.Password);
            else
                user.Password = existUser.Password;

            _unitOfWork.User.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}