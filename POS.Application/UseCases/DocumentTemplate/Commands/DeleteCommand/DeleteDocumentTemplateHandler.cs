using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.DocumentTemplate.Commands.DeleteCommand;

public class DeleteDocumentTemplateHandler : IRequestHandler<DeleteDocumentTemplateCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public DeleteDocumentTemplateHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteDocumentTemplateCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsDocumentTemplate = await _unitOfWork.DocumentTemplate.GetByIdAsync(request.DocumentTemplateId);

            if (existsDocumentTemplate is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            if (existsDocumentTemplate.Content is not null)
                await _fileStorageService.RemoveFile(existsDocumentTemplate.Content!, Containers.DOCUMENT_TEMPLATE);

            await _unitOfWork.DocumentTemplate.DeleteAsync(request.DocumentTemplateId);
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