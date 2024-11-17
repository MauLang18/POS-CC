using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Invoice.Response;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Invoice.Queries.GetByIdQuery;
using POS.Domain.Entities;
using POS.Utilities.Static;
using WatchDog;

public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdQuery, BaseResponse<InvoiceByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetInvoiceByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<BaseResponse<InvoiceByIdResponseDto>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<InvoiceByIdResponseDto>();

        try
        {
            var invoice = await _unitOfWork.Invoice.GetByIdAsync(request.InvoiceId);

            if (invoice == null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var paymentMethod = await _unitOfWork.PaymentMethod.GetByIdAsync(invoice.PaymentMethodId);
            if (paymentMethod != null)
            {
                invoice.PaymentMethod = paymentMethod;
            }

            invoice.AuditCreateDate = invoice.AuditCreateDate.Date;

            response.IsSuccess = true;
            response.Data = _mapper.Map<InvoiceByIdResponseDto>(invoice);
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