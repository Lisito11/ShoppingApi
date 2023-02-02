using System;
namespace ShoppingAPI.Helpers
{
	public class PaginationFilter
	{
        const int MaxPageSize = 100;

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 50;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
        }
    }
}

