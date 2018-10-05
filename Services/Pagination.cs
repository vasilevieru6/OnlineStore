using OnlineShop.Services.ViewMoldels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Services
{
    public static class Pagination
    {

        public static PagedViewModel<T> Paged<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var result = new PagedViewModel<T>
            {
                Count = query.Count(),
                Data = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList()
            };

            return result;
        }

    }
}