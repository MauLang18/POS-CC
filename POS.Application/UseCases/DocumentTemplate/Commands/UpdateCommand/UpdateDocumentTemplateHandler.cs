using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.DocumentTemplate.Commands.UpdateCommand;

public class UpdateDocumentTemplateHandler : IRequestHandler<UpdateDocumentTemplateCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public UpdateDocumentTemplateHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateDocumentTemplateCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existDocumentTemplate = await _unitOfWork.DocumentTemplate.GetByIdAsync(request.DocumentTemplateId);

            if (existDocumentTemplate is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var inventory = _mapper.Map<Entity.DocumentTemplate>(request);
            inventory.Id = request.DocumentTemplateId;

            if (request.Content is not null)
                inventory.Content = await _fileStorageService.EditFile(Containers.DOCUMENT_TEMPLATE, request.Content, existDocumentTemplate.Content!);
            else
                inventory.Content = existDocumentTemplate.Content;

            _unitOfWork.DocumentTemplate.UpdateAsync(inventory);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_UPDATE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}