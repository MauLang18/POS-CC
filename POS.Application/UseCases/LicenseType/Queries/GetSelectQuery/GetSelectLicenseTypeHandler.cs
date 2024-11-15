using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.LicenseType.Queries.GetSelectQuery;

public class GetSelectLicenseTypeHandler : IRequestHandler<GetSelectLicenseTypeQuery, BaseResponse<IEnumerable<SelectResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSelectLicenseTypeHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<SelectResponse>>> Handle(GetSelectLicenseTypeQuery request, CancellationToken cancellationToken)
    {

        var response = new BaseResponse<IEnumerable<SelectResponse>>();

        try
        {
            var categories = await _unitOfWork.LicenseType.GetSelectAsync();

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<SelectResponse>>(categories);
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