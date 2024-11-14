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

    public ReportController(IMediator mediator, IGeneratePdfService generatePdfService)
    {
        _mediator = mediator;
        _generatePdfService = generatePdfService;
    }

    [HttpGet("Pdf/{quoteId:int}")]
    public async Task<IActionResult> QuotePdf(int quoteId)
    {
        // Obtener la cotización usando MediatR
        var response = await _mediator.Send(new GetQuoteByIdQuery { QuoteId = quoteId });

        if (!response.IsSuccess || response.Data == null)
        {
            return NotFound(new { Message = $"No se encontró una cotización con el ID {quoteId}" });
        }

        // Generar el PDF usando el método genérico con el tipo especificado
        byte[] file = await _generatePdfService.GeneratePdf<QuoteByIdResponseDto>(response.Data, 1);

        return File(file, "application/pdf", $"Quote-{quoteId}.pdf");
    }
}
