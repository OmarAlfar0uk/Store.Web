using DomainLayer.Models.ProductModels;
using Service.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductCountSpecification :  BaseSpecifications<Product , int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams)
             : base(P => (!queryParams.BrandId.HasValue || (P.BrandId != null && P.BrandId == queryParams.BrandId))
     &&
     (!queryParams.TypeId.HasValue || (P.TypeId != null && P.TypeId == queryParams.TypeId))
     &&
     (string.IsNullOrWhiteSpace(queryParams.Search) || (P.Name != null && P.Name.ToLower().Contains(queryParams.Search.ToLower()))))
        {
            
        }
    }
}
