using MediatR;
using Microsoft.AspNetCore.Http;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.DocumentTemplate.Commands.UpdateCommand;

public class UpdateDocumentTemplateCommand : IRequest<BaseResponse<bool>>
{
    public int DocumentTemplateId { get; set; }
    public string Name { get; set; } = null!;
    public int TemplateTypeId { get; set; }
    public string Content { get; set; } = null!;
    public int State { get; set; }
}