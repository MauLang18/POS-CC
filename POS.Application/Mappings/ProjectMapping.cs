using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.Project.Response;
using POS.Application.UseCases.Project.Commands.CreateCommand;
using POS.Application.UseCases.Project.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class ProjectMapping : Profile
{
    public ProjectMapping()
    {
        CreateMap<Project, ProjectResponseDto>()
            .ForMember(x => x.ProjectId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Customer, x => x.MapFrom(y => y.Customer.Name))
            .ForMember(x => x.Category, x => x.MapFrom(y => y.Category.Name))
            .ForMember(x => x.Status, x => x.MapFrom(y => y.Status.Name))
            .ForMember(x => x.StateProject, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<Project, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.InternalName))
            .ReverseMap();

        CreateMap<Project, ProjectByIdResponseDto>()
            .ForMember(x => x.ProjectId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<ProjectDetail, ProjectDetailsByIdResponseDto>()
            .ReverseMap();

        CreateMap<CreateProjectCommand, Project>();

        CreateMap<CreateProjectDetailCommand, ProjectDetail>();

        CreateMap<UpdateProjectCommand, Project>();
    }
}