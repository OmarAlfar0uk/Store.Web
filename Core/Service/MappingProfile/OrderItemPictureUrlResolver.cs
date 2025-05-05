using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfile
{
    internal class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDTo, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver( IConfiguration configuration)
        {
           this._configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDTo destination, string destMember, ResolutionContext context)
        {
            
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;
            else
            {
                /*                var Url = $"https://localhost:7061/{source.PictureUrl}";
                */
                var Url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
                return Url;
            }

        }
    }
}
