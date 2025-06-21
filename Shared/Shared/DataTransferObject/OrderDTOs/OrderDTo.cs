using Shared.DataTransferObject.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.OrderDTOs
{
    public class OrderDTo
    {
        public string BasketId { get; set; } = default!;

        public int DeliveryMethodId { get; set; }

        public AddressDto shioToAddress { get; set; } = default!;

    }
}
