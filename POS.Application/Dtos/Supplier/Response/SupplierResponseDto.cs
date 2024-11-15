﻿namespace POS.Application.Dtos.Supplier.Response;

public class SupplierResponseDto
{
    public int SupplierId { get; set; }
    public string? DocumentType { get; set; }
    public string? DocumentNumber { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? ContactName { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime AuditCreateDate { get; set; }
    public int State { get; set; }
    public string? StateSupplier { get; set; }
}