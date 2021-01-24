using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Infrastructure.Data
{
    public class PagedResultModel<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public IEnumerable<T> Items { get; set; }

        public int TotalCount { get; set; }

        public PagedResultModel()
        {

        }

        public PagedResultModel(int size, int index, List<T> items, int totalCount)
        {
            PageSize = size;
            PageIndex = index;
            Items = items;
            TotalCount = totalCount;

        }
    }
}
