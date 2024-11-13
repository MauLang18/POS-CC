using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.DocumentType.Commands.CreateCommand;

public class CreateDocumentTypeCommand : IRequest<BaseResponse<bool>>
{
    public string Name { get; set; } = null!;
    public string Abbreviation { get; set; } = null!;
    public int State { get; set; }
}