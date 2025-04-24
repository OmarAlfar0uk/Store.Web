using DomainLayer.Excptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.ErrorModels;

namespace Store.Web.CustomeMiddleWares
{
    public class CustomeExceptionHandlerMiddleWares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomeExceptionHandlerMiddleWares> _logger;

        public CustomeExceptionHandlerMiddleWares(RequestDelegate Next , ILogger<CustomeExceptionHandlerMiddleWares> Logger)
        {
            _next = Next;
            _logger = Logger;
        }

        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try
            {
               await _next.Invoke(httpContext);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Somthing Went Wrong");
                // Set Status Code For Respons

                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };


                // Set Contant type for Respons
                //httpContext.Response.ContentType = "application/json";
                // Respons Object
                var Response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };
                // Return Object as Json 
               await httpContext.Response.WriteAsJsonAsync(Response);
            }
            
        }
    }
}
