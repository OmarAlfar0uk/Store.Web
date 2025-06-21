using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceMangerWithFactoryDelegate(
        Func<IProductService> ProductFactory,
        Func<IBasketService> BasketFactory,
        Func<IAuthenticationService> AuthenticationFactory,
        Func<IOrederService> OrederFactory ,
        Func<IPaymentService> PaymentFactory) : IServiceManger
    {
        public IProductService ProductService => ProductFactory.Invoke();

        public IBasketService BasketService => BasketFactory.Invoke();

        public IAuthenticationService AuthenticationService =>  AuthenticationFactory.Invoke();

        public IOrederService OrederService => OrederFactory.Invoke();  

        public IPaymentService PaymentService => PaymentFactory.Invoke();
    }
}
