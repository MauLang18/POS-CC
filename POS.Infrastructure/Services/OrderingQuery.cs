﻿using POS.Application.Commons.Bases;
using POS.Application.Interfaces.Services;
using System.Linq.Dynamic.Core;

namespace POS.Infrastructure.Services;

public class OrderingQuery : IOrderingQuery
{
    public IQueryable<T> Ordering<T>(BasePagination request, IQueryable<T> queryable) where T : class
    {
        IQueryable<T> query = request.Order == "desc"
            ? queryable.OrderBy($"{request.Sort} descending")
            : queryable.OrderBy($"{request.Sort} ascending");

        query = query.Paginate(request);

        return query;
    }
}