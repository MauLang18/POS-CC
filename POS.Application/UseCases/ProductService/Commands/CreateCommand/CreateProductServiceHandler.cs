using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.ProductService.Commands.CreateCommand;

public class CreateProductServiceHandler : IRequestHandler<CreateProductServiceCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;
    private readonly IGenerateCodeService _generateCodeService;

    public CreateProductServiceHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService, IGenerateCodeService generateCodeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
        _generateCodeService = generateCodeService;
    }

    public async Task<BaseResponse<bool>> Handle(CreateProductServiceCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var productService = _mapper.Map<Entity.ProductService>(request);
            if (request.Image is not null)
                productService.Image = await _fileStorageService.SaveFile(Containers.PRODUCT_SERVICE, request.Image!);
            productService.Code = await _generateCodeService.GenerateCodeProduct(request.CategoryId);
            await _unitOfWork.ProductService.CreateAsync(productService);
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