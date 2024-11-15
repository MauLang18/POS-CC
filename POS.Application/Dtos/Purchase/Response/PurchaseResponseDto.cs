namespace POS.Application.Dtos.Purchase.Response;

public class PurchaseResponseDto
{
    public int PurchaseId { get; set; }
    public string? Supplier { get; set; }
    public decimal Total { get; set; }
    public DateTime AuditCreateDate { get; set; }
}