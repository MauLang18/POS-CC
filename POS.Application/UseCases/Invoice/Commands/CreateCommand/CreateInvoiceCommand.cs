﻿using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Invoice.Commands.CreateCommand;

public class CreateInvoiceCommand : IRequest<BaseResponse<bool>>
{
    public int VoucherTypeId { get; set; }
    public int InvoiceId { get; set; }
    public string? Observation { get; set; }
    public decimal Total {  get; set; }
    public int InstallmentsCount { get; set; }
    public int PaymentMethodId { get; set; }
    public int StatusId { get; set; }
}