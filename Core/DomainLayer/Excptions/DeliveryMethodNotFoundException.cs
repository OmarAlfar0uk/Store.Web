using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Excptions
{
    public sealed class DeliveryMethodNotFoundException(int id) : NotFoundException($"No DeliveryMethod Find By Id {id}")
    {
    }
}
