using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPipeline.Middleware
{
    class MvcMiddleware
    {
        private readonly RequestDelegate _next;

        public MvcMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(Context context, string extraPara)
        {
            Console.WriteLine($"do MvcMiddleware  {extraPara}");
            _next.Invoke(context);
            return Task.CompletedTask;
        }
    }
}
