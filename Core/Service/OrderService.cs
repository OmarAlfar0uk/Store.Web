using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Excptions;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModels;
using Service.MappingProfile;
using Service.Specifications;
using Service.Specifications.OrderModuleSpecification;
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
    public class OrderService(IMapper _mapper, IBasketRepository _basketRepository, IUnitOfWork _unitOfWork) : IOrederService
    {

        private static OrderItem CreateOrderItem(DomainLayer.Models.BasketModels.BasketItem item, Product Product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrder() { ProductId = Product.Id, PictureUrl = Product.PictureUrl, ProductName = Product.Name },
                Price = Product.Price,
                Quantity = item.Quantity
            };
        }




        public async Task<OrderToReturnDTo> CreateOrderAsync(OrderDTo orderDTo, string Email)
        {
            //mapping address to order address
            var OrderAddress = _mapper.Map<AddressDto, OrderAddress>(orderDTo.shioToAddress);
            //Get Basket
            var Basket =await _basketRepository.GetBasketAsync(orderDTo.BasketId) ?? throw new BasketNotFoundExcptions(orderDTo.BasketId);

            ArgumentNullException.ThrowIfNullOrEmpty(Basket.PaymentIntentId);
            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            var OrderSpec = new OrderWhithPaymentIntentIdSpecifications(Basket.PaymentIntentId);
            var ExtistingOrder =await OrderRepo.GetByIdAsync(OrderSpec);
            if (ExtistingOrder is not null)
                OrderRepo.Remove(ExtistingOrder);

            //Create Order Item List
            List<OrderItem> orderItems =[];
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

            var Order = new Order(Email, OrderAddress, DeliveryMethod, orderItems, SubTotal ,Basket.PaymentIntentId);


           await OrderRepo.AddAsync(Order);
          await  _unitOfWork.SaveChangesAsync();
            return _mapper.Map<Order , OrderToReturnDTo>(Order);
        }

        
        public async Task<IEnumerable<DeliveryMethodDTo>> GetDeliveryMethodAsync()
        {
            var DeliveryMethods =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDTo>>(DeliveryMethods);

        }

        public async Task<IEnumerable<OrderToReturnDTo>> GetAllOrdersAsync(string Email)
        {
            var Spec =  new OrderSpecifications(Email);
            var Orders =await  _unitOfWork.GetRepository<Order , Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDTo>>(Orders);


        }

        public async Task<OrderToReturnDTo> GetOrderByIdAsync(Guid Id)
        {

            var Spec = new OrderSpecifications(Id); 
            var Order =await _unitOfWork.GetRepository<Order , Guid>().GetByIdAsync(Spec);
            return _mapper.Map<Order, OrderToReturnDTo>(Order);

        }
    }
}
