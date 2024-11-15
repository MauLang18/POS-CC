using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.LicenseType.Commands.UpdateCommand;

public class UpdateLicenseTypeHandler : IRequestHandler<UpdateLicenseTypeCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLicenseTypeHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateLicenseTypeCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var licenseType = _mapper.Map<Entity.LicenseType>(request);
            licenseType.Id = request.LicenseTypeId;
            _unitOfWork.LicenseType.UpdateAsync(licenseType);
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