using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace FundooNotes.ExceptionMiddleware
{
    public class CustomExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)ExceptionStatusCode.GetExceptionStatusCode(ex);
                httpContext.Response.ContentType = "application/json";
                var exObj = new {
                    status = false,
                    message = ex.Message,
                    code = httpContext.Response.StatusCode
                };
                await JsonSerializer.SerializeAsync(httpContext.Response.Body, exObj);
            }
        }
       
    }
}
