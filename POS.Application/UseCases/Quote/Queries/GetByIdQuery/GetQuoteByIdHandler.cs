using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Quote.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Quote.Queries.GetByIdQuery;

public class GetQuoteByIdHandler : IRequestHandler<GetQuoteByIdQuery, BaseResponse<QuoteByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetQuoteByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<QuoteByIdResponseDto>> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<QuoteByIdResponseDto>();

        try
        {
            var quote = await _unitOfWork.Quote.GetByIdAsync(request.QuoteId);

            if (quote is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            //var departmentId = await _unitOfWork.Employee.GetByIdAsync(quote.AssignedToId);
            //var locationId = await _unitOfWork.Employee.GetByIdAsync(quote.AssignedToId);
            //var department = await _unitOfWork.Department.GetByIdAsync(departmentId.DepartmentId);
            //var location = await _unitOfWork.Location.GetByIdAsync(locationId.LocationId);
            //var assignedTo = await _unitOfWork.Employee.GetByIdAsync(quote.AssignedToId);
            //var deliveredBy = await _unitOfWork.Users.GetByIdAsync(quote.DeliveredById);
            //var receivedById = await _unitOfWork.Employee.GetByIdAsync(quote.ReceivedById);
            var quoteDetails = await _unitOfWork.QuoteDetail.GetQuoteDetailByQuoteId(request.QuoteId);

            quote.QuoteDetails = quoteDetails.ToList();
            //quote.Department = department.Name;
            //quote.Location = location.Name;
            //quote.AssignedTo = assignedTo.Name + " " + assignedTo.LastName;
            //quote.ReceivedBy = receivedById.Name + " " + receivedById.LastName;
            //quote.DeliveredBy = deliveredBy.Name + " " + deliveredBy.LastName;

            response.IsSuccess = true;
            response.Data = _mapper.Map<QuoteByIdResponseDto>(quote);
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