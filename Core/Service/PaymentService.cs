using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Excptions;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = DomainLayer.Models.ProductModels.Product;
namespace Service
{
    public class PaymentService(IConfiguration _configration , IBasketRepository _basketRepository , IUnitOfWork _unitOfWork , IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey = _configration["StripeSetting:SecretKey"];

            var Basket = await _basketRepository.GetBasketAsync(BasketId) ?? throw new BasketNotFoundExcptions(BasketId);

            var ProudectRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product =await ProudectRepo.GetByIdAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                item.Price = Product.Price;
            }
            ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);  
            var DeliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(Basket.deliveryMethodId.Value)
                ?? throw new DeliveryMethodNotFoundException(Basket.deliveryMethodId.Value);
            Basket.shippingPrice = DeliveryMethod.Price;

            var BasketAmout = (long)(Basket.Items.Sum(items => items.Quantity * items.Price) + DeliveryMethod.Price) * 100;

            var PaymentService = new PaymentIntentService();
            if(Basket.PaymentIntentId is  null) //Create
            {
                var Option = new PaymentIntentCreateOptions()
                {
                    Amount =  BasketAmout ,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"]
                };
                var PaymentIntent = await PaymentService.CreateAsync(Option);
                Basket.PaymentIntentId = PaymentIntent.Id;  
                Basket.clintSecret = PaymentIntent.ClientSecret;
            }
            else //Update
            {
                var Option = new PaymentIntentUpdateOptions() { Amount = BasketAmout };
                await PaymentService.UpdateAsync(Basket.PaymentIntentId, Option);

            }

            await _basketRepository.CreateOrUpdatBaskAsync(Basket);

            return _mapper.Map<BasketDto>(Basket);
        }

    }
}
