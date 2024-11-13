namespace POS.Application.Dtos.Customer.Response;

public class CustomerByIdResponseDto
{
    public int CustomerId { get; set; }
    public int DocumentTypeId { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int? CreditTypeId { get; set; }
    public decimal? DiscountPercent { get; set; }
    public decimal? CreditInterestRate { get; set; }
    public decimal? CreditLimit { get; set; }
    public int State { get; set; }
}