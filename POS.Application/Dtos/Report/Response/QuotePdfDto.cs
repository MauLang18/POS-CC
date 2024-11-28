namespace POS.Application.Dtos.Report.Response;

public class QuotePdfDto
{
    //Customer
    public string? VoucherNumber { get; set; }
    public string? CustomerIdNumber { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAddress { get; set; }
    public string? CustomerPhone { get; set; }
    public string? RequestedBy { get; set; }
    public string? PaymentTerms { get; set; }
    public string? PaymentMethod { get; set; }

    //Quote
    public DateTime AuditCreateDate { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public decimal IVA { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public ICollection<QuoteDetailDto>? QuoteDetails { get; set; }
}

public class QuoteDetailDto
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
}