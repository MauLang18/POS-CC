namespace POS.Application.Dtos.VoucherType.Response;

public class VoucherTypeResponseDto
{
    public int VoucherTypeId { get; set; }
    public string? Name { get; set; }
    public string? Abbreviation { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateVoucherType { get; set; }
}