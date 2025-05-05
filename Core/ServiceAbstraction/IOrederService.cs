using Shared.DataTransferObject.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrederService
    {
        Task<OrderToReturnDTo> CreateOrder(OrderDTo orderDTo, string Email);


    }
}
