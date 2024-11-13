using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.Customer.Response;
using POS.Application.UseCases.Customer.Commands.CreateCommand;
using POS.Application.UseCases.Customer.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class CustomerMapping : Profile
{
    public CustomerMapping()
    {
        CreateMap<Customer, CustomerResponseDto>()
            .ForMember(x => x.CustomerId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.CreditType, x => x.MapFrom(y => y.CreditType.Name))
            .ForMember(x => x.DocumentType, x => x.MapFrom(y => y.DocumentType.Abbreviation))
            .ForMember(x => x.StateCustomer, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<Customer, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<Customer, CustomerByIdResponseDto>()
            .ForMember(x => x.CustomerId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateCustomerCommand, Customer>();

        CreateMap<UpdateCustomerCommand, Customer>();
    }
}