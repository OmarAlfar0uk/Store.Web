using System.Reflection.Metadata.Ecma335;

namespace Shared.DataTransferObject.OrderDTOs
{
    public class OrderItemDTo
    {
        
        public string ProductName { get; set; } = default!;

        public string PictureUrl { get; set; } = default!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}