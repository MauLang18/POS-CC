using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Commons.Select.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.CreditType.Queries.GetSelectQuery;

public class GetSelectCreditTypeHandler : IRequestHandler<GetSelectCreditTypeQuery, BaseResponse<IEnumerable<SelectResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSelectCreditTypeHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<SelectResponse>>> Handle(GetSelectCreditTypeQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<SelectResponse>>();

        try
        {
            var creditTypes = await _unitOfWork.CreditType.GetSelectAsync();

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<SelectResponse>>(creditTypes);
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