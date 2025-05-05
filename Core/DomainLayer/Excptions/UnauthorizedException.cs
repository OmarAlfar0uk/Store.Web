using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Excptions
{
    public class UnauthorizedException(string message = "Invaled Email and Password") : Exception(message)
    {
    }
}
