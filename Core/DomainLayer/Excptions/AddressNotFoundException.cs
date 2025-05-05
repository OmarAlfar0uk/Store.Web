using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Excptions
{
    public sealed class AddressNotFoundException(string UserName) : NotFoundException($"User  {UserName} hHas No Address")
    {
    }
}
