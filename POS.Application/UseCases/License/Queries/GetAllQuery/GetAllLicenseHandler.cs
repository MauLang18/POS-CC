using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.License.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.License.Queries.GetAllQuery;

public class GetAllLicenseHandler : IRequestHandler<GetAllLicenseQuery, BaseResponse<IEnumerable<LicenseResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllLicenseHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<LicenseResponseDto>>> Handle(GetAllLicenseQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<LicenseResponseDto>>();

        try
        {
            var licenses = _unitOfWork.License.GetAllQueryable()
                .Include(x => x.Project)
                .Include(x => x.LicenseType)
                .AsQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        licenses = licenses.Where(x => x.Project.InternalName.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                licenses = licenses.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                licenses = licenses.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, licenses)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await licenses.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<LicenseResponseDto>>(items);
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