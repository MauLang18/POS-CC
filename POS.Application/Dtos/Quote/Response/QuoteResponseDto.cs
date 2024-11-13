namespace POS.Application.Dtos.Quote.Response;

public class QuoteResponseDto
{
    public int QuoteId { get; set; }
    public string? Customer { get; set; }
    public string? VoucherNumber { get; set; }
    public decimal Total { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public string? Status { get; set; }
}