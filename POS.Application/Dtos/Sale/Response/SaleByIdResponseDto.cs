using POS.Application.Dtos.Sale.Response;

namespace POS.Application.Dtos.Sale.Response;

public class SaleByIdResponseDto
{
    public int SaleId { get; set; }
    public string? VoucherNumber { get; set; }
    public int CustomerId { get; set; }
    public int? QuoteId { get; set; }
    public int? ProjectId { get; set; }
    public string? CustomerIdNumber {  get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerPhone { get; set; }
    public string? PaymentTerms { get; set; }
    public string? PaymentMethod { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public int StatusId { get; set; }
    public ICollection<SaleDetailsByIdResponseDto> SaleDetails { get; set; } = null!;
}