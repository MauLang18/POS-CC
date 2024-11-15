using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.LicenseType.Response;
using POS.Application.UseCases.LicenseType.Commands.CreateCommand;
using POS.Application.UseCases.LicenseType.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class LicenseTypeMapping : Profile
{
    public LicenseTypeMapping()
    {
        CreateMap<LicenseType, LicenseTypeResponseDto>()
            .ForMember(x => x.LicenseTypeId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateLicenseType, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<LicenseType, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<LicenseType, LicenseTypeByIdResponseDto>()
            .ForMember(x => x.LicenseTypeId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateLicenseTypeCommand, LicenseType>();

        CreateMap<UpdateLicenseTypeCommand, LicenseType>();
    }
}