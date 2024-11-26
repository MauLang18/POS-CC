using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Domain.Entities;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.Project.Commands.UpdateCommand;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProjectHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var project = _mapper.Map<Entity.Project>(request);
            project.State = (int)StateTypes.Activo;
            project.Id = request.ProjectId;

            project.StartDate = DateTime.SpecifyKind(DateTime.Parse(request.StartDate.ToString()), DateTimeKind.Utc);
            project.EndDate = DateTime.SpecifyKind(DateTime.Parse(request.EndDate.ToString()), DateTimeKind.Utc);

            _unitOfWork.Project.UpdateAsync(project);

            var projectDetails = _mapper.Map<List<ProjectDetail>>(request.ProjectDetails);

            await _unitOfWork.ProjectDetail.UpdateProjectDetails(project.Id, projectDetails);

            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();

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