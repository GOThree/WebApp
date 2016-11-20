using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        HttpStatusCode status = HttpStatusCode.InternalServerError;
        string message = context.Exception.Message;
        HttpResponse response = context.HttpContext.Response;
        response.StatusCode = (int)status;
        response.ContentType = "application/json";
        var err = $"{{\"Message\": \"{message}\"}}";
        response.WriteAsync(err);
    }
}
