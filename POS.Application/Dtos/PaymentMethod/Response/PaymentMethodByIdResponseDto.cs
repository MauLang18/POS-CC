namespace POS.Application.Dtos.PaymentMethod.Response;

public class PaymentMethodByIdResponseDto
{
    public int PaymentMethodId { get; set; }
    public string? Name { get; set; }
    public int State { get; set; }
}