namespace POS.Domain.Entities;

public class DocumentTemplate : BaseEntity
{
    public string Name { get; set; } = null!;
    public int TemplateTypeId { get; set; }
    public string Content { get; set; } = null!;

    public virtual TemplateType TemplateType { get; set; } = null!;
}