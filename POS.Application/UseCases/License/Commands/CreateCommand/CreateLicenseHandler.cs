using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.License.Commands.CreateCommand;

public class CreateLicenseHandler : IRequestHandler<CreateLicenseCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IGenerateCodeService _generateCodeService;

    public CreateLicenseHandler(IUnitOfWork unitOfWork, IMapper mapper, IGenerateCodeService generateCodeService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _generateCodeService = generateCodeService;
    }

    public async Task<BaseResponse<bool>> Handle(CreateLicenseCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var licenseCode = await _generateCodeService.GenerateSoftwareLicense(request.ProjectId, request.LicenseTypeId);

            var license = _mapper.Map<Entity.License>(request);
            license.LicenseKey = licenseCode;

            await _unitOfWork.License.CreateAsync(license);
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