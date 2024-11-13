using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Customer.Commands.CreateCommand;

public class CreateCustomerCommand : IRequest<BaseResponse<bool>>
{
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int CreditTypeId { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal CreditInterestRate { get; set; }
    public decimal CreditLimit { get; set; }
    public int State { get; set; }
}