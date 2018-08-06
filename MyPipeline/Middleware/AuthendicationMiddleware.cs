using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPipeline.Middleware
{
    class AuthendicationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthendicationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(Context context, string extraPara)
        {
            Console.WriteLine($"do AuthendicationMiddleware  {extraPara}");
            _next.Invoke(context);
            return Task.CompletedTask;
        }
    }
}
