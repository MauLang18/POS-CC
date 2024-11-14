namespace POS.Application.Dtos.Quote.Response;

public class QuoteByIdResponseDto
{
    public int QuoteId { get; set; }
    public string? VoucherNumber { get; set; }
    public int CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerPhone { get; set; }
    public string? RequestedBy { get; set; }
    public string? PaymentTerms { get; set; }
    public string? PaymentMethod { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public int StatusId { get; set; }
    public ICollection<QuoteDetailsByIdResponseDto> QuoteDetails { get; set; } = null!;
}
