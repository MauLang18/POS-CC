using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Quote.Response;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Quote.Queries.GetByIdQuery;

namespace InvenTrackCore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IGeneratePdfService _generatePdfService;
    private readonly IEmailService _emailService;

    public ReportController(IMediator mediator, IGeneratePdfService generatePdfService, IEmailService emailService)
    {
        _mediator = mediator;
        _generatePdfService = generatePdfService;
        _emailService = emailService;
    }

    [HttpGet("Cotizacion/{quoteId:int}")]
    public async Task<IActionResult> QuotePdf(int quoteId)
    {
        var response = await _mediator.Send(new GetQuoteByIdQuery { QuoteId = quoteId });

        if (!response.IsSuccess || response.Data == null)
        {
            return NotFound(new { Message = $"No se encontró una cotización con el ID {quoteId}" });
        }

        byte[] file = await _generatePdfService.GeneratePdf<QuoteByIdResponseDto>(response.Data, 1);

        var customerEmail = response.Data.CustomerEmail;
        if (string.IsNullOrEmpty(customerEmail))
        {
            return BadRequest(new { Message = "No se proporcionó un correo electrónico para el cliente." });
        }

        try
        {
            await _emailService.SendEmail(response.Data, 1, file, customerEmail, response.Data.VoucherNumber!);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"Error al enviar el correo: {ex.Message}" });
        }

        return File(file, "application/pdf", $"{response.Data.VoucherNumber}.pdf");
    }

    //[HttpGet("Factura/{quoteId:int}")]
    //public async Task<IActionResult> InvoicePdf(int invoiceId)
    //{
    //    var response = await _mediator.Send(new GetInvoiceByIdQuery { InvoiceId = invoiceId });

    //    if (!response.IsSuccess || response.Data == null)
    //    {
    //        return NotFound(new { Message = $"No se encontró una factura con el ID {invoiceId}" });
    //    }

    //    byte[] file = await _generatePdfService.GeneratePdf<InvoiceByIdResponseDto>(response.Data, 1);

    //    return File(file, "application/pdf", $"{response.Data.VoucherNumber}.pdf");
    //}
}
