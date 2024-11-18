using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.License.Response;
using POS.Application.UseCases.License.Commands.CreateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class LicenseMapping : Profile
{
    public LicenseMapping()
    {
        CreateMap<License, LicenseResponseDto>()
            .ForMember(x => x.LicenseId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.LicenseType, x => x.MapFrom(y => y.LicenseType.Name))
            .ForMember(x => x.Project, x => x.MapFrom(y => y.Project.InternalName))
            .ForMember(x => x.StateLicense, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<License, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.LicenseKey))
            .ReverseMap();

        CreateMap<License, LicenseByIdResponseDto>()
            .ForMember(x => x.LicenseId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateLicenseCommand, License>();
    }
}