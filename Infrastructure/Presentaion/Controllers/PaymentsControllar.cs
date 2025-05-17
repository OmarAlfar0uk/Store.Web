using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class PaymentsControllar(IServiceManger _serviceManger) : ApiBaseControlar
    {
        [Authorize]
        [HttpPost("{BasketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            var Basket = await _serviceManger.PaymentService.CreateOrUpdatePaymentIntentAsync(BasketId);
            return Ok(Basket);
        }
    }
}
