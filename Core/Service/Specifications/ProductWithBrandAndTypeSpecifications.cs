using DomainLayer.Models.ProductModels;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product ,  int>
    {
        //Get All Product With Brand And Type 
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base(P => (!queryParams.BrandId.HasValue || (P.BrandId != null && P.BrandId == queryParams.BrandId))
    &&
    (!queryParams.TypeId.HasValue || (P.TypeId != null && P.TypeId == queryParams.TypeId))
    &&
    (string.IsNullOrWhiteSpace(queryParams.Search) || (P.Name != null && P.Name.ToLower().Contains(queryParams.Search.ToLower()))))

        {
            AddInclode(P => P.ProductBrand);
            AddInclode(P => P.ProductType);

            switch (queryParams.sort) 
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    break;

            }

            ApplyPagination(queryParams.PageSize, queryParams.PageSize);
        }

        //Get Product By Id

        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclode(P => P.ProductBrand);
            AddInclode(P => P.ProductType);
        }
    }
}
