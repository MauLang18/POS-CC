namespace POS.Application.Dtos.Purchase.Response;

public class PurchaseByIdResponseDto
{
    public int PurchaseId { get; set; }
    public int SupplierId { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public ICollection<PurchaseDetailsByIdResponseDto> PurchaseDetails { get; set; } = null!;
}