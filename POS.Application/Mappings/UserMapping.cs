using AutoMapper;
using POS.Application.Dtos.User.Response;
using POS.Application.UseCases.User.Commands.CreateCommand;
using POS.Application.UseCases.User.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<User, UserResponseDto>()
            .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StateUser, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
        .ReverseMap();

        CreateMap<User, UserByIdResponseDto>()
            .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateUserCommand, User>();

        CreateMap<UpdateUserCommand, User>();
    }
}