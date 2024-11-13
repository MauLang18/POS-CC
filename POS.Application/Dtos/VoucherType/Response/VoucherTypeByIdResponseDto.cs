namespace POS.Application.Dtos.VoucherType.Response;

public class VoucherTypeByIdResponseDto
{
    public int VoucherTypeId { get; set; }
    public string? Name { get; set; }
    public string? Abbreviation { get; set; }
    public int State { get; set; }
}