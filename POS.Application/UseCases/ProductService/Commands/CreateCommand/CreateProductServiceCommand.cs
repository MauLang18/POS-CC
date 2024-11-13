﻿using Microsoft.AspNetCore.Http;

namespace POS.Application.UseCases.ProductService.Commands.CreateCommand;

public class CreateProductServiceCommand
{
    public string Name { get; set; } = null!;
    public IFormFile? Image { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int UnitId { get; set; }
    public int IsService { get; set; }
    public int StockQuantity { get; set; }
    public int State { get; set; }
}