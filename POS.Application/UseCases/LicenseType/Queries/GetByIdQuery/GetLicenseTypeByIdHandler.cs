using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.LicenseType.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.LicenseType.Queries.GetByIdQuery;

public class GetLicenseTypeByIdHandler : IRequestHandler<GetLicenseTypeByIdQuery, BaseResponse<LicenseTypeByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLicenseTypeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<LicenseTypeByIdResponseDto>> Handle(GetLicenseTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<LicenseTypeByIdResponseDto>();

        try
        {
            var licenseType = await _unitOfWork.LicenseType.GetByIdAsync(request.LicenseTypeId);

            if (licenseType is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<LicenseTypeByIdResponseDto>(licenseType);
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