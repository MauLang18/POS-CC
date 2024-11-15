namespace POS.Application.Dtos.Sale.Response;

public class SaleResponseDto
{
    public int SaleId { get; set; }
    public string? Customer { get; set; }
    public string? VoucherNumber { get; set; }
    public decimal Total { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public string? Status { get; set; }
}