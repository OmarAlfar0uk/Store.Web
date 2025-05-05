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

    public class BasketController(IServiceManger _serviceManger) : ApiBaseConttrolar
    {
        [HttpGet]

        public async Task<ActionResult<BasketDto>> GetBasket(string Key)
        {
            var Basket =await _serviceManger.BasketService.GetBasketAsync(Key);
            return Ok(Basket);  
        }

        [HttpPost]

        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto Basket) 
        {
            var basket =await _serviceManger.BasketService.CreateOrUpdateBasketAsync(Basket);
            return Ok(basket);
        }

        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        { 
            var Result =await _serviceManger.BasketService.DeleteBasketAsync(Key);
            return Ok(Result);
        }
    }
}
