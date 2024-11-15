using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.LicenseType.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.LicenseType.Queries.GetAllQuery;

public class GetAllLicenseTypeHandler : IRequestHandler<GetAllLicenseTypeQuery, BaseResponse<IEnumerable<LicenseTypeResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllLicenseTypeHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<LicenseTypeResponseDto>>> Handle(GetAllLicenseTypeQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<LicenseTypeResponseDto>>();

        try
        {
            var licenseTypes = _unitOfWork.LicenseType.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        licenseTypes = licenseTypes.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                licenseTypes = licenseTypes.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                licenseTypes = licenseTypes.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, licenseTypes)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await licenseTypes.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<LicenseTypeResponseDto>>(items);
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