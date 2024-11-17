using POS.Application.Dtos.Sale.Response;

namespace POS.Application.Dtos.Invoice.Response;

public class InvoicePdfDto
{
    // Datos de Invoice
    public string VoucherNumber { get; set; } = null!;
    public int InstallmentsCount { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }

    // Datos de Sale
    public string? CustomerIdNumber { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerPhone { get; set; }
    public string? PaymentTerms { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public ICollection<SaleDetailsByIdResponseDto> SaleDetails { get; set; } = null!;
}