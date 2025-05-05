using AutoMapper;
using DomainLayer.Models.IdentityModule;
using Shared.DataTransferObject.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfile
{
    public class IdentityProfile :Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address , AddressDto>().ReverseMap();

        }
    }
}
