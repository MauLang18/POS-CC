using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.DocumentTemplate.Commands.DeleteCommand;

public class DeleteDocumentTemplateCommand : IRequest<BaseResponse<bool>>
{
    public int DocumentTemplateId { get; set; }
}