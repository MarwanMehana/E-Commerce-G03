using AutoMapper;
using DomainLayer.Contarcts;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork unitOfWork, IMapper mapper,IBasketRepository basketRepository) : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService = new Lazy<IProductService>(() => new ProductService(unitOfWork, mapper));

        public IProductService ProductService => _lazyProductService.Value;
        private readonly Lazy<IBasketService> _lazyBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository,mapper)) ;

        public IBasketService BasketService => _lazyBasketService.Value;
    }
}
