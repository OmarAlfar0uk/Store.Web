using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Excptions;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModels;
using Service.MappingProfile;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDTOs;
using Shared.DataTransferObject.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper _mapper  , IBasketRepository _basketRepository , IUnitOfWork _unitOfWork ) : IOrederService
    {
        public async Task<OrderToReturnDTo> CreateOrder(OrderDTo orderDTo, string Email)
        {
            //mapping address to order address
            var OrderAddress =  _mapper.Map<AddressDto , OrderAddress>(orderDTo.Address);
            //Get Basket
            var Basket =await _basketRepository.GetBasketAsync(orderDTo.BasketId) 
            ?? throw new BasketNotFoundExcptions(orderDTo.BasketId);
            //Create Order Item List
            List<OrderItem> orderItems =[ ];
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                orderItems.Add(CreateOrderItem(item, Product));
            }
            //Get Delivery method
            var DeliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDTo.DeliveryMethodId)
                  ?? throw new DeliveryMethodNotFoundException(orderDTo.DeliveryMethodId);
            //Calcolate sub Total
            var SubTotal = orderItems.Sum(I => I.Quantity * I.Price);

            var Order = new Order(Email, OrderAddress, DeliveryMethod, orderItems, SubTotal);


           await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);
          await  _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order , OrderToReturnDTo>(Order);
        }

        private static OrderItem CreateOrderItem(DomainLayer.Models.BasketModels.BasketItem item, Product Product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrder() { ProductId = Product.Id, PictureUrl = Product.PictureUrl, ProductName = Product.Name },
                Price = Product.Price,
                Quantity = item.Quantity
            };
        }
    }
}
