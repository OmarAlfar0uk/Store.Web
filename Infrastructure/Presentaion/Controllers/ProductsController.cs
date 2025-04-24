using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject.ProductMoodulDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController (IServiceManger _serviceManger) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
          var products= await  _serviceManger.ProductService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProducts(int id) 
        {
            var Product =await _serviceManger.ProductService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>>GetType()
        {
            var Types = await _serviceManger.ProductService.GetAllTypessAsync();
            return Ok(Types);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable <BrandDto>>> GetBrands() 
        {
            var Brands =await _serviceManger.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }
    }
}
