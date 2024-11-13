using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.DocumentType.Commands.DeleteCommand;

public class DeleteDocumentTypeCommand : IRequest<BaseResponse<bool>>
{
    public int DocumentTypeId { get; set; }
}