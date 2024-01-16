using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using SQLitePCL;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ILogger<ExceptionMiddleware> Logger;
        public IHostEnvironment Env ;
        
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger,IHostEnvironment env)
        {
            this.Env = env;
            this.Logger = logger;
            this._next = next;
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
        
        try 
        {
        await _next(context);
        }
        catch(Exception ex)
        {
         Logger.LogError(ex,ex.Message);
         context.Response.ContentType="application/json";
         context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

         var response=Env.IsDevelopment() 
         ? new ApiException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString()):new ApiException((int)HttpStatusCode.InternalServerError);


        var options=new JsonSerializerOptions {PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

        var json=JsonSerializer.Serialize(response,options);

         await context.Response.WriteAsync(json);
        }

        }
        
    }
}