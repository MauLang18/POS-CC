using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Quote.Response;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Quote.Queries.GetByIdQuery;
using POS.Domain.Entities;
using POS.Utilities.Static;
using WatchDog;

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

            if (quote == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var customer = await _unitOfWork.Customer.GetByIdAsync(quote.CustomerId);

            if (customer != null)
            {
                quote.Customer = customer;
            }

            var quoteDetails = await _unitOfWork.QuoteDetail.GetQuoteDetailByQuoteId(request.QuoteId);
            quote.QuoteDetails = quoteDetails.ToList();

            response.IsSuccess = true;
            response.Data = _mapper.Map<QuoteByIdResponseDto>(quote);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}