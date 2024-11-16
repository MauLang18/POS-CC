using AutoMapper;
using POS.Application.Dtos.Invoice.Response;
using POS.Application.UseCases.Invoice.Commands.CreateCommand;
using POS.Domain.Entities;

namespace POS.Application.Mappings;

public class InvoiceMapping : Profile
{
    public InvoiceMapping()
    {
        CreateMap<Invoice, InvoiceResponseDto>()
            .ForMember(x => x.InvoiceId, x => x.MapFrom(y => y.Id))
            .ForMember(x => x.Sale, x => x.MapFrom(y => y.Sale.VoucherNumber))
            .ForMember(x => x.Status, x => x.MapFrom(y => y.Status.Name))
            .ReverseMap();

        CreateMap<Invoice, InvoiceByIdResponseDto>()
            .ForMember(x => x.InvoiceId, x => x.MapFrom(y => y.Id))
            .ReverseMap();

        CreateMap<CreateInvoiceCommand, Invoice>();
    }
}