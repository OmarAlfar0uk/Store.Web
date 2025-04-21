using DomainLayer.Models;
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
            : base(P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams. BrandId)
            &&
              (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId)
            &&
            (string.IsNullOrWhiteSpace(queryParams.SearchValue)) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))
            
        {
            AddInclode(P => P.ProductBrand);
            AddInclode(P => P.ProductType);

            switch (queryParams.sortingOptions) 
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
        }

        //Get Product By Id

        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclode(P => P.ProductBrand);
            AddInclode(P => P.ProductType);
        }
    }
}
