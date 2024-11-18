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

        try
        {
            var project = _mapper.Map<Entity.Project>(request);

            var startDate = DateTime.SpecifyKind(DateTime.Parse(request.StartDate.ToString()), DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(DateTime.Parse(request.EndDate.ToString()), DateTimeKind.Utc);

            project.StartDate = startDate;
            project.EndDate = endDate;

            await _unitOfWork.Project.CreateAsync(project);
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