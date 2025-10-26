using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        // Get Basket
        [HttpGet] // GET : baseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }

        // Create Or Update Basket
        [HttpPost] // POST : baseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var createdBasket = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(createdBasket);
        }

        // Delete Basket
        [HttpDelete("{key}")] // DELETE : baseUrl/api/Basket/basket01
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var result = await _serviceManager.BasketService.DeleteBasketAsync(key);
            return Ok(result);
        }

    }
}
