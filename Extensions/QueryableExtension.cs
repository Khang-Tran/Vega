using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject vehicleQuery, Dictionary<string, Expression<Func<T, object>>> columnMap)
        {
            if (String.IsNullOrWhiteSpace(vehicleQuery.SortBy) || !columnMap.ContainsKey(vehicleQuery.SortBy))
                return query;
            query = vehicleQuery.IsAscending
                ? query.OrderBy(columnMap[vehicleQuery.SortBy])
                : query.OrderByDescending(columnMap[vehicleQuery.SortBy]);
            return query;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {
            if (queryObj.Page <= 0)
                queryObj.Page = 1;
            if (queryObj.PageSize <= 0)
                queryObj.PageSize = 10; 

            return query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);

        }
    }
}
