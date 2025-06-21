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

namespace Presentaion.Attribute
{
    internal class CacheAttribute(int DurationInSac = 90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string cacheKey= CreateCacheKey(context.HttpContext.Request);

            ICacheService cacheService =context.HttpContext.RequestServices.GetRequiredService<ICacheService>(); 
            
            var cacheValue =await cacheService.GetAsync(cacheKey);

            if (cacheValue is not null) 
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            
            }
            var ExecutedContext =await  next.Invoke();

            if (ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cacheKey,(string) result.Value , TimeSpan.FromSeconds(DurationInSac));
            }

        }
        private string CreateCacheKey(HttpRequest Request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(Request.Path + '&');
            foreach (var item in Request.Query.OrderBy(Q => Q.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");
            }
            return Key.ToString();  
        }
    }
}
 