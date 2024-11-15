using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Supplier.Commands.CreateCommand;

public class CreateSupplierCommand : IRequest<BaseResponse<bool>>
{
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string ContactName { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int State { get; set; }
}