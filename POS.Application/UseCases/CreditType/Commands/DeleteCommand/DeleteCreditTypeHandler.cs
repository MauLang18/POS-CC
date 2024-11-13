using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.CreditType.Commands.DeleteCommand;

public class DeleteCreditTypeHandler : IRequestHandler<DeleteCreditTypeCommand, BaseResponse<bool>>
{
    private IUnitOfWork _unitOfWork;

    public DeleteCreditTypeHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteCreditTypeCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsCreditType = await _unitOfWork.CreditType.GetByIdAsync(request.CreditTypeId);

            if (existsCreditType is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            await _unitOfWork.CreditType.DeleteAsync(request.CreditTypeId);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}