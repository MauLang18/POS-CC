namespace POS.Application.Dtos.Supplier.Response;

public class SupplierByIdResponseDto
{
    public int SupplierId { get; set; }
    public int DocumentTypeId { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? ContactName { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public int State { get; set; }
}