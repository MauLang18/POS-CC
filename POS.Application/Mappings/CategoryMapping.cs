using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.Category.Response;
using POS.Application.UseCases.Category.Commands.CreateCommand;
using POS.Application.UseCases.Category.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class CategoryMapping : Profile
{
    public CategoryMapping()
    {
        CreateMap<Category, CategoryResponseDto>()
            .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<Category, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<Category, CategoryByIdResponseDto>()
            .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateCategoryCommand, Category>();

        CreateMap<UpdateCategoryCommand, Category>();
    }
}