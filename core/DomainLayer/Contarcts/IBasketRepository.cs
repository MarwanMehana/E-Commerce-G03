using DomainLayer.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contarcts
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(string key);
        Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string key);
    }
}
