using DomainLayer.Models;
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
        public ProductWithBrandAndTypeSpecifications():base(null)
        {
            AddInclode(P => P.ProductBrand);
            AddInclode(P => P.ProductType);
        }

        //Get Product By Id

        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclode(P => P.ProductBrand);
            AddInclode(P => P.ProductType);
        }
    }
}
