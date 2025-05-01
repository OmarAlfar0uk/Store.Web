using AutoMapper;
using DomainLayer.Models.BasketModels;
using Shared.DataTransferObject.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfile
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem , BasketItemDto>().ReverseMap();   
        }
    }
}
