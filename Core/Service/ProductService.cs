using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDto= _mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);   
            return BrandsDto;   
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Specification = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Products =await Repo.GetAllAsync(Specification);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductCount = Products.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var TotalCount =await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(queryParams.PageIndex, ProductCount, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypessAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Specification = new ProductWithBrandAndTypeSpecifications(id);  
            var Product =await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specification);
            return _mapper.Map<Product, ProductDto>(Product);   
        }
    }
}
