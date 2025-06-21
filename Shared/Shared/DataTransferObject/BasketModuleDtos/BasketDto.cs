using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.BasketModuleDtos
{
    public class BasketDto
    {
        public string Id { get; set; }

        public ICollection<BasketItemDto> Items { get; set; } = [];

        public string? clintSecret { get; set; }

        public string? PaymentIntentId { get; set; }

        public int? deliveryMethodId { get; set; }

        public decimal? shippingPrice { get; set; }

    }
}
