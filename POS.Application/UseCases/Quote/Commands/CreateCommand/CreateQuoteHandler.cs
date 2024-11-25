using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.Quote.Commands.CreateCommand;

public class CreateQuoteHandler : IRequestHandler<CreateQuoteCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenerateCodeService _generateCodeService;

    public CreateQuoteHandler(IUnitOfWork unitOfWork, IMapper mapper, IGenerateCodeService generateCodeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _generateCodeService = generateCodeService;
    }

    public async Task<BaseResponse<bool>> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var quote = _mapper.Map<Entity.Quote>(request);
            quote.State = (int)StateTypes.Activo;

            quote.VoucherNumber = await _generateCodeService.GenerateCodeQuote(quote.Id);

            await _unitOfWork.Quote.CreateAsync(quote);
            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();
            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}