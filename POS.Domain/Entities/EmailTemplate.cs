namespace POS.Domain.Entities;

public class EmailTemplate : BaseEntity
{
    public int TemplateTypeId { get; set; }
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;

    public virtual TemplateType TemplateType { get; set; } = null!;
}