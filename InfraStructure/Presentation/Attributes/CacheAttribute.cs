using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    internal class CacheAttribute(int DurationInSec=120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Create Cache Key
            string cacheKey = CreateCacheKey(context.HttpContext.Request);

            // Search for value with Cache Key
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cachevalue = await cacheService.GetAsync(cacheKey);

            // Return Value if Not Null
            if (cachevalue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cachevalue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }            
            // If Null
            // Invoke Next
            var ExecutedContext = await next.Invoke();
            // Set Value(Response) with Cache Key
            if (ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cacheKey, result, TimeSpan.FromSeconds(DurationInSec));
            }            
            // Return Value
        }

        private string CreateCacheKey(HttpRequest request)
        {
            // /api/Products
            // /api/Products?brandId=2&typeId=1
            // /api/Products?typeId=1&brandId=2

            StringBuilder key = new StringBuilder();
            key.Append(request.Path);
            key.Append("?");
            foreach (var item in request.Query.OrderBy(Q => Q.Key))
            {
                key.Append($"{item.Key}$={item.Value}$");
            }
            return key.ToString();
        }
    }
}