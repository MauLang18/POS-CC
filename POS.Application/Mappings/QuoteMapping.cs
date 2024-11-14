using AutoMapper;
using POS.Application.Dtos.Quote.Response;
using POS.Application.UseCases.Quote.Commands.CreateCommand;
using POS.Domain.Entities;

namespace POS.Application.Mappings;

public class QuoteMapping : Profile
{
    public QuoteMapping()
    {
        CreateMap<Quote, QuoteResponseDto>()
            .ForMember(x => x.QuoteId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Customer, x => x.MapFrom(y => y.Customer.Name))
            .ForMember(x => x.Status, x => x.MapFrom(y => y.Status.Name))
            .ReverseMap();

        CreateMap<Quote, QuoteByIdResponseDto>()
            .ForMember(x => x.QuoteId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.CustomerName, x => x.MapFrom(y => y.Customer.Name))
            .ForMember(x => x.CustomerAddress, x => x.MapFrom(y => y.Customer.Address))
            .ForMember(x => x.CustomerPhone, x => x.MapFrom(y => y.Customer.Phone))
            .ForMember(x => x.PaymentMethod, x => x.MapFrom(y => y.PaymentMethod.Name))
            .ForMember(x => x.PaymentTerms, x => x.MapFrom(y => y.Customer.CreditType.Name))
            .ForMember(x => x.RequestedBy, x => x.MapFrom(y => y.Customer.ContactName))
            .ForMember(x => x.AuditCreateDate, x => x.MapFrom(y => y.AuditCreateDate.ToString("yyyy-MM-dd"))) // Solo fecha
            .ReverseMap();

        CreateMap<QuoteDetail, QuoteDetailsByIdResponseDto>()
            .ForMember(x => x.ProductServiceId, x => x.MapFrom(y => y.ProductServiceId))
            .ForMember(x => x.Code, x => x.MapFrom(y => y.ProductService.Code))
            .ForMember(x => x.Name, x => x.MapFrom(y => y.ProductService.Name))
            .ForMember(x => x.Image, x => x.MapFrom(y => y.ProductService.Image))
            .ReverseMap();

        CreateMap<CreateQuoteCommand, Quote>();

        CreateMap<CreateQuoteDetailCommand, QuoteDetail>();
    }
}