using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Filters
{
    public class SampleAsyncActionFilter : IAsyncActionFilter
    {
        private int counter = 0;
        private static int limit = 5;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (counter >= limit)
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            else {
                counter++;
                try
                {
                    await next();
                }
                finally
                {
                    counter--;
                }
            }
        }
    }
}
