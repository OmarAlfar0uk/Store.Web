using Shared.DataTransferObject.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.OrderDTOs
{
    public class OrderToReturnDTo
    {
        public Guid Id  { get; set; }

        public string buyerEmail { get; set; } = default!;

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public AddressDto shipToAddress { get; set; } = default!;

        public string  DeliveryMethod { get; set; } = default!;

        public decimal deliveryCost { get; set; } = default!;

        public int DeliveryMethodId { get; set; }  //fk

        public string Status { get; set; } = default!;

        public ICollection<OrderItemDTo> Items { get; set; } = [];

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }
    }
}
