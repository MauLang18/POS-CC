using MediatR;
using Microsoft.AspNetCore.Http;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.DocumentTemplate.Commands.CreateCommand;

public class CreateDocumentTemplateCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public int TemplateTypeId { get; set; }
    public IFormFile Content { get; set; } = null!;
    public int State { get; set; }
}