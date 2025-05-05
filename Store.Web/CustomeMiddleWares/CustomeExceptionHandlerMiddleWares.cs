using Azure;
using DomainLayer.Excptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.ErrorModels;

namespace Store.Web.CustomeMiddleWares
{
    public class CustomeExceptionHandlerMiddleWares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomeExceptionHandlerMiddleWares> _logger;

        public CustomeExceptionHandlerMiddleWares(RequestDelegate Next, ILogger<CustomeExceptionHandlerMiddleWares> Logger)
        {
            _next = Next;
            _logger = Logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

                await HandelNotFoundEndPointAsync(httpContext);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Somthing Went Wrong");
                await HandelExceptionAsync(httpContext, ex);
            }

        }

        private static async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {


            // Set Contant type for Respons
            //httpContext.Response.ContentType = "application/json";
            // Respons Object
            var Response = new ErrorToReturn()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = ex.Message
            };
            // Set Status Code For Respons

            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException, Response),
                _ => StatusCodes.Status500InternalServerError
            };



            // Return Object as Json 
            await httpContext.Response.WriteAsJsonAsync(Response);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;

        }

        private static async Task HandelNotFoundEndPointAsync(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Respons = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpContext.Request.Path} Is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Respons);
            }
        }
    }
}