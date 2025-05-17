using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class OrderWhithPaymentIntentIdSpecifications : BaseSpecifications<Order , Guid>
    {
        public OrderWhithPaymentIntentIdSpecifications(string PaymentIntetnId): base(O=>O.PaymentIntentId == PaymentIntetnId)
        {
            
        }
    }
}
