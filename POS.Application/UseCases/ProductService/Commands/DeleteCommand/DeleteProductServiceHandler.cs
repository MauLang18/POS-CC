using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.ProductService.Commands.DeleteCommand;

public class DeleteProductServiceHandler : IRequestHandler<DeleteProductServiceCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public DeleteProductServiceHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteProductServiceCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsProductService = await _unitOfWork.ProductService.GetByIdAsync(request.ProductServiceId);

            if (existsProductService is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            if (existsProductService.Image is not null)
                await _fileStorageService.RemoveFile(existsProductService.Image!, Containers.PRODUCT_SERVICE);

            await _unitOfWork.ProductService.DeleteAsync(request.ProductServiceId);
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