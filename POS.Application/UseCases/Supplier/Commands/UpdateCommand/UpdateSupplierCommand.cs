using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Supplier.Commands.UpdateCommand;

public class UpdateSupplierCommand : IRequest<BaseResponse<bool>>
{
    public int SupplierId { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Email { get; set; }
    public string ContactName { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int State { get; set; }
}