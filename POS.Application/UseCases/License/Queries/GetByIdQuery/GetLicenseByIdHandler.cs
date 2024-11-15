using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.License.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.License.Queries.GetByIdQuery;

public class GetLicenseByIdHandler : IRequestHandler<GetLicenseByIdQuery, BaseResponse<LicenseByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLicenseByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<LicenseByIdResponseDto>> Handle(GetLicenseByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<LicenseByIdResponseDto>();

        try
        {
            var license = await _unitOfWork.License.GetByIdAsync(request.LicenseId);

            if (license is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<LicenseByIdResponseDto>(license);
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