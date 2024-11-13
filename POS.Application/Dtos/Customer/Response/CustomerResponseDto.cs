namespace POS.Application.Dtos.Customer.Response;

public class CustomerResponseDto
{
    public int CustomerId { get; set; }
    public string? DocumentType { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? CreditType { get; set; }
    public decimal? DiscountPercent { get; set; }
    public decimal? CreditInterestRate { get; set; }
    public decimal? CreditLimit { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateCustomer { get; set; }
}