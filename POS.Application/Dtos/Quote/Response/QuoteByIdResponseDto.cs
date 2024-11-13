namespace POS.Application.Dtos.Quote.Response;

public class QuoteByIdResponseDto
{
    public int QuoteId { get; set; }
    public string? VoucherNumber { get; set; }
    public int CustomerId { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public int StatusId { get; set; }
}