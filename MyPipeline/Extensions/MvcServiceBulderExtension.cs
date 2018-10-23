using MyPipeline.Middleware;
using MyPipeline.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPipeline.Extensions
{
    static class MvcServiceBulderExtension
    {
        public static void AddMvc(this ServiceBuilder  s,string extraPara)
        {
            //Console.WriteLine(extraPara);
            s.Use(next =>
               {
                   return context =>
                   {
                       MvcMiddleware middleware = new MvcMiddleware(next);
                       middleware.Invoke(context,extraPara);
                       return Task.CompletedTask;
                   };
               }

            );
        }
    }
}
