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
            DoProccess1();
        }
        static void DoProccess2()
        {
            Use((next) =>
            {
                return context =>
                {
                    //处理 start logic1, 进入next处理， next.invoke结束后处理 stop logic2
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
                    Task ins = next.Invoke(context);
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
            foreach (var middleware in _list)
            {
                //Func<RequestDelegate , RequestDelegate>  输入 RequestDelegate 输出RequestDelegate
                //通过Reverse  然后从尾部传入 end , middleware.Invoke(end) 传入了 每个middleware的RequestDelegate next参数
                //使得整条链接形成通路
                end = middleware.Invoke(end);
            }
            //调用的时候 end 中的 next 已经在之前 _list 遍历中传入 
            end.Invoke(new Context());
            Console.ReadLine();
        }

        static void DoProccess1()
        {

            Use((next) => {
                return (context) =>
                {
                    Console.WriteLine("tran begin");
                    Task t = next.Invoke(context);
                    Console.WriteLine("tran end");
                    return t;
                };
            });

            Use((next) => {
                {
                    Console.WriteLine("log begin");
                    Task t = next.Invoke(context);
                    return t;
                };
            });
            RequestDelegate end = (context) => {
                Console.WriteLine("ending ....");
                return Task.CompletedTask;
            };

            _list.Reverse();
            foreach (var middleware in _list)
            {
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
