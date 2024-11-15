namespace POS.Application.Dtos.Invoice.Response;

public class InvoiceResponseDto
{
    public int InvoiceId { get; set; }
    public string? Sale { get; set; }
    public string? VoucherNumber { get; set; }
    public decimal Total { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public string? Status { get; set; }
}