using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.DocumentTemplate.Commands.CreateCommand;

public class CreateDocumentTemplateHandler : IRequestHandler<CreateDocumentTemplateCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public CreateDocumentTemplateHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<BaseResponse<bool>> Handle(CreateDocumentTemplateCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var documentTemplate = _mapper.Map<Entity.DocumentTemplate>(request);
            if (request.Content is not null)
                documentTemplate.Content = await _fileStorageService.SaveFile(Containers.DOCUMENT_TEMPLATE, request.Content);
            await _unitOfWork.DocumentTemplate.CreateAsync(documentTemplate);
            await _unitOfWork.SaveChangesAsync();

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