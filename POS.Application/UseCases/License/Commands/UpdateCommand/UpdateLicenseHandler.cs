using AutoMapper;
using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;
using Entity = POS.Domain.Entities;

namespace POS.Application.UseCases.License.Commands.UpdateCommand;

public class UpdateLicenseHandler : IRequestHandler<UpdateLicenseCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateLicenseHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(UpdateLicenseCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var license = _mapper.Map<Entity.License>(request);
            license.Id = request.LicenseId;
            _unitOfWork.License.UpdateAsync(license);
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