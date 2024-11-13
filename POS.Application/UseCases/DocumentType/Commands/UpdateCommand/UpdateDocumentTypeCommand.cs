using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.DocumentType.Commands.UpdateCommand;

public class UpdateDocumentTypeCommand : IRequest<BaseResponse<bool>>
{
    public int DocumentTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string? Abbreviation { get; set; }
    public int State { get; set; }
}