﻿using MediatR;
using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.UseCases.Category.Commands.DeleteCommand;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var existsCategory = await _unitOfWork.Category.GetByIdAsync(request.CategoryId);

            if (existsCategory is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            await _unitOfWork.Category.DeleteAsync(request.CategoryId);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_DELETE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}