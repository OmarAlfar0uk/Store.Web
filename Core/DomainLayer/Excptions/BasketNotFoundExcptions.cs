using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Excptions
{
    public sealed class BasketNotFoundExcptions(string id) : NotFiniteNumberException($"Basket With id {id}  Is Not Found")
    {
    }
}
