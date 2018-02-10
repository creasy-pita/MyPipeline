using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyPipeline
{
    class Program
    {
        public static List<Func<RequestDelegate, RequestDelegate>> _list = new List<Func<RequestDelegate, RequestDelegate>>();

        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            Use((next) =>
            {
                return context =>
                {
                    Console.WriteLine("11  doing my  start logic");
                    Task ins = next.Invoke(context);
                    Console.WriteLine("11 doing my  stop logic");
                    return ins;
                };
            });
            Use((next) =>
            {
                return context =>
                {
                    Console.WriteLine("22  doing my  start logic");
                    Task ins= next.Invoke(context);
                    Console.WriteLine("22 doing my  stop logic");
                    return ins;
                };
            });
            RequestDelegate end = (context =>
                {
                    Console.WriteLine("end...");
                    return Task.CompletedTask;
                }
            );
            _list.Reverse();
            foreach(var middleware in _list)
            {
                //Func<RequestDelegate , RequestDelegate>  输入 RequestDelegate 输出RequestDelegate
                //通过Reverse  然后从尾部传入 end , middleware.Invoke(end) 传入了 每个middleware的RequestDelegate next参数
                //使得整条链接形成通路
                end = middleware.Invoke(end);
            }
            end.Invoke(new Context());
            Console.ReadLine();

        }

        //传入Func<RequestDelegate , RequestDelegate>类型的 middleware， 加入_list 但还没有调用，待使用
        public static  void Use(Func<RequestDelegate , RequestDelegate> middleware)
        {
            _list.Add(middleware);
        }



    }
}
