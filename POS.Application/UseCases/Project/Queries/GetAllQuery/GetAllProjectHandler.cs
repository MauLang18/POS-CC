using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Project.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Project.Queries.GetAllQuery;

public class GetAllProjectHandler : IRequestHandler<GetAllProjectQuery, BaseResponse<IEnumerable<ProjectResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllProjectHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<ProjectResponseDto>>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<ProjectResponseDto>>();

        try
        {
            var projects = _unitOfWork.Project.GetAllQueryable()
                .Include(x => x.Customer)
                .Include(x => x.Status)
                .Include(x => x.Category)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        projects = projects.Where(x => x.InternalName.Contains(request.TextFilter));
                        break;
                    case 2:
                        projects = projects.Where(x => x.CommercialName!.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                projects = projects.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                projects = projects.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, projects)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await projects.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<ProjectResponseDto>>(items);
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