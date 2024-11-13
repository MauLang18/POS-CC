using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.ProductService.Commands.UpdateCommand;

public class UpdateProductServiceHandler : IRequestHandler<UpdateProductServiceCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;
    private readonly IGenerateCodeService _generateCodeService;

    public UpdateProductServiceHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService, IGenerateCodeService generateCodeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
        _generateCodeService = generateCodeService;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateProductServiceCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existProductService = await _unitOfWork.ProductService.GetByIdAsync(request.ProductServiceId);

            if (existProductService is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            var productService = _mapper.Map<Entity.ProductService>(request);
            productService.Id = request.ProductServiceId;

            if (request.CategoryId.Equals(existProductService.CategoryId))
                productService.Code = existProductService.Code;
            else
                productService.Code = await _generateCodeService.GenerateCodeProduct(request.CategoryId);

            if (request.Image is not null)
                productService.Image = await _fileStorageService.EditFile(Containers.PRODUCT_SERVICE, request.Image, existProductService.Image!);
            else
                productService.Image = existProductService.Image;

            _unitOfWork.ProductService.UpdateAsync(productService);
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