namespace POS.Application.Dtos.PaymentMethod.Response;

public class PaymentMethodResponseDto
{
    public int PaymentMethodId { get; set; }
    public string? Name {  get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StatePaymentMethod {  get; set; }
}