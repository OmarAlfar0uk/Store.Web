using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiBaseConttrolar : ControllerBase
    {
        protected string GetEmailFromToken()=> User.FindFirstValue(ClaimTypes.Email)!;
    }
}
