using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Project.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Project.Queries.GetByIdQuery;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, BaseResponse<ProjectByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProjectByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<ProjectByIdResponseDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<ProjectByIdResponseDto>();

        try
        {
            var project = await _unitOfWork.Project.GetByIdAsync(request.ProjectId);

            if (project is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<ProjectByIdResponseDto>(project);
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