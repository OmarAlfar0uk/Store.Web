using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public  class ProductQueryParams
    {
        private const int DafaultPageSize = 5;
        private const int MaxPageSize = 10;

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public ProductSortingOptions sort { get; set; }

        public string? Search { get; set; }

        public int PageNumber { get; set; } = 1;

        private int pageSize = DafaultPageSize;
        public int PageSize
        {
            get {  return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
    }
}
