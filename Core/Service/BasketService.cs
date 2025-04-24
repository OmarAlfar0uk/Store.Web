using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Excptions;
using DomainLayer.Models.BasketModels;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class BasketService(IBasketRepository _basketRepository , IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomarBasket = _mapper.Map<BasketDto , CustomerBasket>(basket);
            var CreateOrUpdatedBasket =await _basketRepository.CreateOrUpdatBaskAsync(CustomarBasket);
            if (CreateOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can not Updated Or Created Basked Now , Blease Try Again Later");
        }


        public async Task<BasketDto> GetBasketAsync(string Key)
        {
            var Basket =await _basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDto>(Basket);
            else
                throw new BasketNotFoundExcptions(Key);
        }

        public async Task<bool> DeleteBasketAsync(string Key) => await _basketRepository.DeletBasketAsync(Key);

    }
}
