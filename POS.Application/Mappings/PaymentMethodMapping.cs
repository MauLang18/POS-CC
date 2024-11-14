using AutoMapper;
using POS.Application.Commons.Select.Response;
using POS.Application.Dtos.PaymentMethod.Response;
using POS.Application.UseCases.PaymentMethod.Commands.CreateCommand;
using POS.Application.UseCases.PaymentMethod.Commands.UpdateCommand;
using POS.Domain.Entities;
using POS.Utilities.Static;

namespace POS.Application.Mappings;

public class PaymentMethodMapping : Profile
{
    public PaymentMethodMapping()
    {
        CreateMap<PaymentMethod, PaymentMethodResponseDto>()
            .ForMember(x => x.PaymentMethodId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.StatePaymentMethod, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Activo) ? "ACTIVO" : "INACTIVO"))
            .ReverseMap();

        CreateMap<PaymentMethod, SelectResponse>()
            .ForMember(x => x.Id, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Description, x => x.MapFrom(y => y.Name))
            .ReverseMap();

        CreateMap<PaymentMethod, PaymentMethodByIdResponseDto>()
            .ForMember(x => x.PaymentMethodId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreatePaymentMethodCommand, PaymentMethod>();

        CreateMap<UpdatePaymentMethodCommand, PaymentMethod>();
    }
}