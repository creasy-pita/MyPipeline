using MyPipeline.Middleware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyPipeline.Extensions
{
    static class ProgramExtension
    {
        public static void UseMvc(this Program p,string extraPara)
        {
            
            //Program.Use(next =>
            //    {
            //        return context =>
            //        {
            //            MvcMiddleware middleware = new MvcMiddleware(next);
            //            next.Invoke(context);
            //            return Task.CompletedTask;
            //        };
            //    }

                
            //    );
        }
    }
}
