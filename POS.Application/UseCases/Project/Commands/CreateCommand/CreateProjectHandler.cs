using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.Project.Commands.CreateCommand;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProjectHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var project = _mapper.Map<Entity.Project>(request);
            project.State = (int)StateTypes.Activo;

            project.StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
            project.EndDate = DateTime.SpecifyKind(request.EndDate, DateTimeKind.Utc);

            await _unitOfWork.Project.CreateAsync(project);
            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();
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