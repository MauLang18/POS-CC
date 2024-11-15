using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using BC = BCrypt.Net.BCrypt;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.User.Commands.CreateCommand;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var user = _mapper.Map<Entity.User>(request);
            user.Password = BC.HashPassword(user.Password);

            await _unitOfWork.User.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}