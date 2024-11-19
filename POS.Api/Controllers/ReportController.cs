using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Invoice.Response;
using POS.Application.Dtos.Quote.Response;
using POS.Application.Dtos.Sale.Response;
using POS.Application.Interfaces.Services;
using POS.Application.UseCases.Invoice.Queries.GetByIdQuery;
using POS.Application.UseCases.Quote.Queries.GetByIdQuery;
using POS.Application.UseCases.Sale.Queries.GetByIdQuery;

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

    [HttpGet("CotizacionEmail/{quoteId:int}")]
    public async Task<IActionResult> QuoteEmailPdf(int quoteId)
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

    [HttpGet("Cotizacion/{quoteId:int}")]
    public async Task<IActionResult> QuotePdf(int quoteId)
    {
        var response = await _mediator.Send(new GetQuoteByIdQuery { QuoteId = quoteId });

        if (!response.IsSuccess || response.Data == null)
        {
            return NotFound(new { Message = $"No se encontró una cotización con el ID {quoteId}" });
        }

        byte[] file = await _generatePdfService.GeneratePdf<QuoteByIdResponseDto>(response.Data, 1);

        return File(file, "application/pdf", $"{response.Data.VoucherNumber}.pdf");
    }

    [HttpGet("FacturaEmail/{invoiceId:int}")]
    public async Task<IActionResult> InvoiceEmailPdf(int invoiceId)
    {
        var response = await _mediator.Send(new GetInvoiceByIdQuery { InvoiceId = invoiceId });
        if (!response.IsSuccess || response.Data == null)
        {
            return NotFound(new { Message = $"No se encontró una factura con el ID {invoiceId}" });
        }

        var sale = await _mediator.Send(new GetSaleByIdQuery { SaleId = response.Data!.SaleId });
        if (sale == null || sale.Data == null)
        {
            return NotFound(new { Message = $"No se encontró una venta asociada con la factura ID {invoiceId}" });
        }

        var installmentsCount = response.Data.InstallmentsCount;
        if (installmentsCount < 1) installmentsCount = 1;

        var installmentTotal = Math.Round(sale.Data.Total / installmentsCount, 2);
        var installmentIVA = Math.Round(sale.Data.IVA / installmentsCount, 2);
        var installmentSubTotal = Math.Round(sale.Data.SubTotal / installmentsCount, 2);

        var dividedDetails = new List<SaleDetailsByIdResponseDto>();

        foreach (var detail in sale.Data.SaleDetails)
        {
            int baseQuantity = detail.Quantity / installmentsCount;
            int remainingQuantity = detail.Quantity % installmentsCount;

            var totalForInstallment = Math.Round(detail.Total / installmentsCount, 2);

            dividedDetails.Add(new SaleDetailsByIdResponseDto
            {
                ProductServiceId = detail.ProductServiceId,
                Image = detail.Image,
                Code = detail.Code,
                Name = detail.Name,
                Quantity = baseQuantity + (remainingQuantity > 0 ? 1 : 0),
                Price = detail.Price,
                Total = totalForInstallment
            });

            remainingQuantity--;
        }

        var invoicePdfData = new InvoicePdfDto
        {
            VoucherNumber = response.Data.VoucherNumber!,
            InstallmentsCount = response.Data.InstallmentsCount,
            IssueDate = response.Data.IssueDate,
            DueDate = response.Data.IssueDate.AddDays(15),

            CustomerIdNumber = sale.Data!.CustomerIdNumber,
            CustomerName = sale.Data.CustomerName,
            CustomerEmail = sale.Data.CustomerEmail,
            CustomerAddress = sale.Data.CustomerAddress,
            CustomerPhone = sale.Data.CustomerPhone,
            PaymentTerms = sale.Data.PaymentTerms,
            PaymentMethod = response.Data.PaymentMethod,
            IVA = installmentIVA,
            Total = installmentTotal,
            Observation = sale.Data.Observation,
            SubTotal = installmentSubTotal,
            SaleDetails = dividedDetails
        };

        byte[] file = await _generatePdfService.GeneratePdf<InvoicePdfDto>(invoicePdfData, 2);

        var customerEmail = sale.Data.CustomerEmail;
        if (string.IsNullOrEmpty(customerEmail))
        {
            return BadRequest(new { Message = "No se proporcionó un correo electrónico para el cliente." });
        }

        try
        {
            await _emailService.SendEmail(invoicePdfData, 2, file, customerEmail, response.Data.VoucherNumber!);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"Error al enviar el correo: {ex.Message}" });
        }

        return File(file, "application/pdf", $"{response.Data.VoucherNumber}.pdf");
    }

    [HttpGet("Factura/{invoiceId:int}")]
    public async Task<IActionResult> InvoicePdf(int invoiceId)
    {
        var response = await _mediator.Send(new GetInvoiceByIdQuery { InvoiceId = invoiceId });
        if (!response.IsSuccess || response.Data == null)
        {
            return NotFound(new { Message = $"No se encontró una factura con el ID {invoiceId}" });
        }

        var sale = await _mediator.Send(new GetSaleByIdQuery { SaleId = response.Data!.SaleId });
        if (sale == null || sale.Data == null)
        {
            return NotFound(new { Message = $"No se encontró una venta asociada con la factura ID {invoiceId}" });
        }

        var installmentsCount = response.Data.InstallmentsCount;
        if (installmentsCount < 1) installmentsCount = 1;

        var installmentTotal = Math.Round(sale.Data.Total / installmentsCount, 2);
        var installmentIVA = Math.Round(sale.Data.IVA / installmentsCount, 2);
        var installmentSubTotal = Math.Round(sale.Data.SubTotal / installmentsCount, 2);

        var dividedDetails = new List<SaleDetailsByIdResponseDto>();

        foreach (var detail in sale.Data.SaleDetails)
        {
            int baseQuantity = detail.Quantity / installmentsCount;
            int remainingQuantity = detail.Quantity % installmentsCount;

            var totalForInstallment = Math.Round(detail.Total / installmentsCount, 2);

            dividedDetails.Add(new SaleDetailsByIdResponseDto
            {
                ProductServiceId = detail.ProductServiceId,
                Image = detail.Image,
                Code = detail.Code,
                Name = detail.Name,
                Quantity = baseQuantity + (remainingQuantity > 0 ? 1 : 0),
                Price = detail.Price,
                Total = totalForInstallment
            });

            remainingQuantity--;
        }

        var invoicePdfData = new InvoicePdfDto
        {
            VoucherNumber = response.Data.VoucherNumber!,
            InstallmentsCount = response.Data.InstallmentsCount,
            IssueDate = response.Data.IssueDate,
            DueDate = response.Data.IssueDate.AddDays(15),

            CustomerIdNumber = sale.Data!.CustomerIdNumber,
            CustomerName = sale.Data.CustomerName,
            CustomerEmail = sale.Data.CustomerEmail,
            CustomerAddress = sale.Data.CustomerAddress,
            CustomerPhone = sale.Data.CustomerPhone,
            PaymentTerms = sale.Data.PaymentTerms,
            PaymentMethod = response.Data.PaymentMethod,
            IVA = installmentIVA,
            Total = installmentTotal,
            Observation = sale.Data.Observation,
            SubTotal = installmentSubTotal,
            SaleDetails = dividedDetails
        };

        byte[] file = await _generatePdfService.GeneratePdf<InvoicePdfDto>(invoicePdfData, 2);

        var customerEmail = sale.Data.CustomerEmail;
        if (string.IsNullOrEmpty(customerEmail))
        {
            return BadRequest(new { Message = "No se proporcionó un correo electrónico para el cliente." });
        }

        try
        {
            await _emailService.SendEmail(invoicePdfData, 2, file, customerEmail, response.Data.VoucherNumber!);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = $"Error al enviar el correo: {ex.Message}" });
        }

        return File(file, "application/pdf", $"{response.Data.VoucherNumber}.pdf");
    }
}
