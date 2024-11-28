using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Invoice.Response;
using POS.Application.Dtos.Quote.Response;
using POS.Application.Dtos.Report.Response;
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

    [HttpGet("Download/{id:int}")]
    public async Task<IActionResult> DownloadPdf(int id, [FromQuery] string reportType, int templateId)
    {
        var (isSuccess, data) = await GetReportData(id, reportType);
        if (!isSuccess || data == null)
            return NotFound(new { Message = $"No se encontró un reporte de tipo {reportType} con el ID {id}" });

        // Verificamos si el tipo de data es InvoicePdfDto o QuotePdfDto
        if (data is InvoicePdfDto invoiceData)
        {
            try
            {
                byte[] file = await _generatePdfService.GeneratePdf(invoiceData, templateId);
                return File(file, "application/pdf", $"{invoiceData.VoucherNumber}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al generar el PDF: {ex.Message}" });
            }
        }
        else if (data is QuotePdfDto quoteData)
        {
            try
            {
                byte[] file = await _generatePdfService.GeneratePdf(quoteData, templateId);
                return File(file, "application/pdf", $"{quoteData.VoucherNumber}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al generar el PDF: {ex.Message}" });
            }
        }
        else
        {
            return StatusCode(500, new { Message = "Tipo de reporte no reconocido." });
        }
    }

    [HttpGet("SendEmail/{id:int}")]
    public async Task<IActionResult> SendPdfByEmail(int id, [FromQuery] string reportType, int templateId)
    {
        var (isSuccess, data) = await GetReportData(id, reportType);
        if (!isSuccess || data == null)
            return NotFound(new { Message = $"No se encontró un reporte de tipo {reportType} con el ID {id}" });

        // Verificamos si el tipo de data es InvoicePdfDto o QuotePdfDto
        if (data is InvoicePdfDto invoiceData)
        {
            if (string.IsNullOrEmpty(invoiceData.CustomerEmail))
                return BadRequest(new { Message = "No se proporcionó un correo electrónico para el cliente." });

            try
            {
                byte[] file = await _generatePdfService.GeneratePdf(invoiceData, templateId);
                await _emailService.SendEmail(invoiceData, templateId, file, invoiceData.CustomerEmail!, invoiceData.VoucherNumber!);
                return Ok(new { Message = "Correo enviado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al enviar el correo: {ex.Message}" });
            }
        }
        else if (data is QuotePdfDto quoteData)
        {
            if (string.IsNullOrEmpty(quoteData.CustomerEmail))
                return BadRequest(new { Message = "No se proporcionó un correo electrónico para el cliente." });

            try
            {
                byte[] file = await _generatePdfService.GeneratePdf(quoteData, templateId);
                await _emailService.SendEmail(quoteData, templateId, file, quoteData.CustomerEmail!, quoteData.VoucherNumber!);
                return Ok(new { Message = "Correo enviado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al enviar el correo: {ex.Message}" });
            }
        }
        else
        {
            return StatusCode(500, new { Message = "Tipo de reporte no reconocido." });
        }
    }

    [HttpGet("ViewPdf/{id:int}")]
    public async Task<IActionResult> ViewPdf(int id, [FromQuery] string reportType, int templateId)
    {
        var (isSuccess, data) = await GetReportData(id, reportType);
        if (!isSuccess || data == null)
            return NotFound(new { Message = $"No se encontró un reporte de tipo {reportType} con el ID {id}" });

        // Verificamos si el tipo de data es InvoicePdfDto o QuotePdfDto
        if (data is InvoicePdfDto invoiceData)
        {
            try
            {
                byte[] file = await _generatePdfService.GeneratePdf(invoiceData, templateId);
                return File(file, "application/pdf"); // Para visualizar directamente en el navegador
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al generar el PDF: {ex.Message}" });
            }
        }
        else if (data is QuotePdfDto quoteData)
        {
            try
            {
                byte[] file = await _generatePdfService.GeneratePdf(quoteData, templateId);
                return File(file, "application/pdf"); // Para visualizar directamente en el navegador
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error al generar el PDF: {ex.Message}" });
            }
        }
        else
        {
            return StatusCode(500, new { Message = "Tipo de reporte no reconocido." });
        }
    }

    private async Task<(bool isSuccess, object? data)> GetReportData(int id, string reportType)
    {
        switch (reportType.ToLower())
        {
            case "invoice":
                var invoiceResponse = await _mediator.Send(new GetInvoiceByIdQuery { InvoiceId = id });
                if (!invoiceResponse.IsSuccess || invoiceResponse.Data == null)
                    return (false, null);

                var saleResponse = await _mediator.Send(new GetSaleByIdQuery { SaleId = invoiceResponse.Data.SaleId });
                if (!saleResponse.IsSuccess || saleResponse.Data == null)
                    return (false, null);

                return (true, PrepareInvoiceData(invoiceResponse.Data, saleResponse.Data));

            case "quote":
                var quoteResponse = await _mediator.Send(new GetQuoteByIdQuery { QuoteId = id });
                if (!quoteResponse.IsSuccess || quoteResponse.Data == null)
                    return (false, null);

                return (true, PrepareQuoteData(quoteResponse.Data));

            default:
                return (false, null);
        }
    }

    private InvoicePdfDto PrepareInvoiceData(InvoiceByIdResponseDto invoice, SaleByIdResponseDto sale)
    {
        var installmentsCount = invoice.InstallmentsCount > 0 ? invoice.InstallmentsCount : 1;

        var dividedDetails = new List<SaleDetailsByIdResponseDto>();

        foreach (var detail in sale.SaleDetails)
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

        return new InvoicePdfDto
        {
            VoucherNumber = invoice.VoucherNumber,
            InstallmentsCount = installmentsCount,
            IssueDate = invoice.IssueDate,
            DueDate = invoice.IssueDate,
            CustomerIdNumber = sale.CustomerIdNumber,
            CustomerName = sale.CustomerName,
            CustomerEmail = sale.CustomerEmail,
            CustomerAddress = sale.CustomerAddress,
            CustomerPhone = sale.CustomerPhone,
            PaymentTerms = sale.PaymentTerms,
            PaymentMethod = invoice.PaymentMethod,
            IVA = Math.Round(sale.IVA / installmentsCount, 2),
            Total = Math.Round(sale.Total / installmentsCount, 2),
            SubTotal = Math.Round(sale.SubTotal / installmentsCount, 2),
            Observation = sale.Observation,
            SaleDetails = dividedDetails
        };
    }

    private QuotePdfDto PrepareQuoteData(QuoteByIdResponseDto quote)
    {
        // Asegúrate de que QuoteDetailsByIdResponseDto se convierte a QuoteDetailDto
        var quoteDetails = quote.QuoteDetails.Select(detail => new QuoteDetailDto
        {
            Name = detail.Name,
            Code = detail.Code,
            Price = detail.Price,
            Quantity = detail.Quantity,
            Total = detail.Total
        }).ToList();

        return new QuotePdfDto
        {
            VoucherNumber = quote.VoucherNumber,
            AuditCreateDate = quote.AuditCreateDate,
            CustomerName = quote.CustomerName,
            RequestedBy = quote.RequestedBy,
            CustomerEmail = quote.CustomerEmail,
            CustomerAddress = quote.CustomerAddress,
            CustomerPhone = quote.CustomerPhone,
            PaymentTerms = quote.PaymentTerms,
            PaymentMethod = quote.PaymentMethod,
            Observation = quote.Observation,
            SubTotal = quote.SubTotal,
            IVA = quote.IVA,
            Discount = quote.Discount,
            Total = quote.Total,
            QuoteDetails = quoteDetails
        };
    }
}