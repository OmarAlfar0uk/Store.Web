using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DataTransferObject.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfile
{
    public class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>();
        }
    }
}
