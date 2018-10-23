using MyPipeline.Middleware;
using MyPipeline.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPipeline.Extensions
{
    static class AuthendicationServiceBulderExtension
    {
        public static void AddAuthendication(this ServiceBuilder  s,string extraPara)
        {
            //Console.WriteLine(extraPara);
            s.Use(next =>
            {
                return context =>
                {
                    AuthendicationMiddleware middleware = new AuthendicationMiddleware(next);
                    middleware.Invoke(context, extraPara);
                    return Task.CompletedTask;
                };
            }

            );
        }
    }
}
