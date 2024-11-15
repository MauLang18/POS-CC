﻿using MediatR;
using POS.Application.Commons.Bases;

namespace POS.Application.UseCases.Project.Commands.UpdateCommand;

public class UpdateProjectCommand : IRequest<BaseResponse<bool>>
{
    public int ProjectId { get; set; }
    public string InternalName { get; set; } = null!;
    public string CommercialName { get; set; } = null!;
    public int CustomerId { get; set; }
    public int CategoryId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int StatusId { get; set; }
    public int State { get; set; }
}