﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Response;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Category.Queries.GetAllQuery;

public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, BaseResponse<IEnumerable<CategoryResponseDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IOrderingQuery _ordering;

    public GetAllCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IOrderingQuery ordering)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _ordering = ordering;
    }

    public async Task<BaseResponse<IEnumerable<CategoryResponseDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<IEnumerable<CategoryResponseDto>>();

        try
        {
            var categories = _unitOfWork.Category.GetAllQueryable();

            if (request.NumFilter is not null && !string.IsNullOrEmpty(request.TextFilter))
            {
                switch (request.NumFilter)
                {
                    case 1:
                        categories = categories.Where(x => x.Name.Contains(request.TextFilter));
                        break;
                }
            }

            if (request.StateFilter is not null)
            {
                categories = categories.Where(x => x.State == request.StateFilter);
            }

            if (!string.IsNullOrEmpty(request.StartDate) && !string.IsNullOrEmpty(request.EndDate))
            {
                categories = categories.Where(x => x.AuditCreateDate >= Convert.ToDateTime(request.StartDate).ToUniversalTime() &&
                                                     x.AuditCreateDate <= Convert.ToDateTime(request.EndDate).ToUniversalTime().AddDays(1));
            }

            request.Sort ??= "Id";

            var items = await _ordering.Ordering(request, categories)
                .ToListAsync(cancellationToken);

            response.IsSuccess = true;
            response.TotalRecords = await categories.CountAsync(cancellationToken);
            response.Data = _mapper.Map<IEnumerable<CategoryResponseDto>>(items);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}