using Shared.DataTransferObject.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrederService
    {
        Task<OrderToReturnDTo> CreateOrderAsync(OrderDTo orderDTo, string Email);

        Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodAsync();

        Task<IEnumerable<OrderToReturnDTo>> GetAllOrdersAsync(string Email);    

        Task<OrderToReturnDTo>  GetOrderByIdAsync(Guid Id);

    }
}
