using AutoMapper;
using DomainLayer.Models.ProductModels;
using Microsoft.Extensions.Options;
using Shared.DataTransferObject.ProductMoodulDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.ProductBrand, Options => Options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.ProductType, Options => Options.MapFrom(src => src.ProductType.Name))
                .ForMember(dist => dist.PictureUrl, Options => Options.MapFrom<PictureUrlResolver>());


            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType,TypeDto>();
        }
    }
}
