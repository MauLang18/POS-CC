﻿namespace POS.Application.Dtos.Invoice.Response;

public class InvoiceByIdResponseDto
{
    public int InvoiceId { get; set; }
    public int VoucherTypeId { get; set; }
    public string? VoucherNumber { get; set; }
    public int SaleId { get; set; }
    public decimal Total { get; set; }
    public string? PaymentMethod { get; set; }
    public int StatusId { get; set; }
    public int InstallmentsCount { get; set; }
    public int PaymentMethodId { get; set; }
    public DateTime PaymentDate { get; set; }
    public DateTime IssueDate { get; set; }
}