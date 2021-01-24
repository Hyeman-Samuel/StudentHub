using StudentHub.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentHub.Infrastructure.Extensions
{
    public static class QueryExtensions
    {
        public static PagedResultModel<T> Paginate<T>(this IOrderedQueryable<T> query, int pageIndex, int pageSize) where T : class
        {
            var result = new PagedResultModel<T>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            result.TotalCount = query.Count();
            var skip = (pageIndex - 1) * pageSize;

            result.Items = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }


    }
}
