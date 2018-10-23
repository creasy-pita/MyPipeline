using MyPipeline.Middleware;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPipeline.Extensions;
using MyPipeline.Services;

namespace MyPipeline
{
    class Program
    {
        private static ServiceBuilder _serviceBuilder;
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            _serviceBuilder = new ServiceBuilder();
            DoProcess4();
        }

        static void DoProcess3()
        {
            UseSayAWord("you");
            UseSayAWord("complete");

            UseSayAWord("me");
            UseCustomMiddleWare("this is extra middleware");
            _serviceBuilder.Run();
        }

        static void UseCustomMiddleWare(string aa)
        {
            //RequestDelegate next1 = context1 => { string ss = ""; return Task.CompletedTask; };
            //RequestDelegate r = context1 =>
            //{
            //    CustomMiddleware middleware = new CustomMiddleware(next1);
            //    Task t = middleware.Invoke(context1, " this is extra middleware");
            //    return t;
            //};



            _serviceBuilder.Use(next =>
                context => {
                    AuthendicationMiddleware middleware = new AuthendicationMiddleware(next);
                    return middleware.Invoke(context, aa);
                }
            );
        }


        static void UseSayAWord(string word)
        {
                    _serviceBuilder.Use(next =>
            {
                return (context=>
                {
                    Console.Write(word+ " ");
                    Task t= next.Invoke(context);
                    return t;
                });
        
            }
            );


            _serviceBuilder.Use( next=> {
                return (context => 
                            {
                                Console.Write("");
                                Task t= next.Invoke(context);
                                return t;
                            });
            });
            _serviceBuilder.Use(next => { return ggg; });//可以返回定义好 符合 RequestDelegate 的 函数
        }

        static void UseSayAwords(string ss)
        {
            _serviceBuilder.Use(_next =>
            {
                return (options => { Console.WriteLine(ss); return _next.Invoke(options); }

                );
            }
                );
        }

        static void DoProccess2()
        {
            _serviceBuilder.Use((next) =>
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
            _serviceBuilder.Use((next) =>
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
            //_list.Reverse();
            //foreach (var middleware in _list)
            //{
            //    //Func<RequestDelegate , RequestDelegate>  输入 RequestDelegate 输出RequestDelegate
            //    //通过Reverse  然后从尾部传入 end , middleware.Invoke(end) 传入了 每个middleware的RequestDelegate next参数
            //    //使得整条链接形成通路
            //    end = middleware.Invoke(end);
            //}
            ////调用的时候 end 中的 next 已经在之前 _list 遍历中传入 
            //end.Invoke(new Context());
            //Console.ReadLine();
        }

        static void DoProccess1()
        {

            _serviceBuilder.Use((next) => {
                return (context) =>
                {
                    Console.WriteLine("tran begin");
                    Task t = next.Invoke(context);
                    Console.WriteLine("tran end");
                    return t;
                };
            });

            _serviceBuilder.Use((next) => {
                return (context) =>
                {
                    Console.WriteLine("log begin");
                    Task t = next.Invoke(context);
                    Console.WriteLine("log end");
                    return t;
                };
            });
            RequestDelegate end = (context) => {
                Console.WriteLine("ending ....");
                return Task.CompletedTask;
            };

            //_list.Reverse();
            //foreach (var middleware in _list)
            //{
            //    end = middleware.Invoke(end);
            //}
            //end.Invoke(new Context());
            //Console.ReadLine();
        }


        static void DoProcess4()
        {
            _serviceBuilder.AddMvc("Mvc  donging ...");
            _serviceBuilder.AddAuthendication("Authendication doing...");
            _serviceBuilder.Run();
        }

        //传入Func<RequestDelegate , RequestDelegate>类型的 middleware， 加入_list 但还没有调用，待使用
        public static Task ggg(Context context)
        {
            return Task.CompletedTask;
        }
    }
}
