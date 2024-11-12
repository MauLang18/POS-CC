﻿namespace POS.Domain.Entities;

public class Invoice : BaseEntity
{
    public int VoucherTypeId { get; set; }
    public string VoucherNumber { get; set; } = null!;
    public int SaleId { get; set; }
    public string? Observation { get; set; }
    public decimal SubTotal { get; set; }
    public int ApplyIVA { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public int StatusId { get; set; }
    public int InstallmentsCount { get; set; }
    public int PaymentMethodId { get; set; }
    public DateTime PaymentDate { get; set; }

    public virtual VoucherType VoucherType { get; set; } = null!;
    public virtual Sale Sale { get; set; } = null!;
    public virtual Status Status { get; set; } = null!;
    public virtual PaymentMethod PaymentMethod { get; set; } = null!;
    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
}