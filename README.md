# MyPipeline
1 使用use方法 添加进中间件
2 invoke时 按链式使用中间件的功能



Fun<RequestDelegate,RequestDelegate>  a
	调用时需要输入一个 RequestDelegate 类型的 方法
	并输出一个 RequestDelegate 类型的 方法
List<Fun<RequestDelegate,RequestDelegate>> list用于存放中间件的 列表
中间件m1,m2,m3...,使用 Fun<RequestDelegate,RequestDelegate> 的定义，

Use() 时 使用匿名方式定义中间件， 并加入到 list
	比如
		static void UseSayAWord(string word)//内部使用Use ，并出入变量word
		{
			Use(next =>
			{
				return (context=>
				{
					Console.Write(word+ " ");
					Task t= next.Invoke(context);

					return t;
				});
				
			}
			);
		}	


从用途  角度 去解释 中间件	
	需要函数与函数的连接
	函数代码逻辑块是可以自定义的
	函数有类型定义上的约束 
		
	所以定义一个 List<Fun<RequestDelegate,RequestDelegate> >
	
	
	c版 mypipeline 实现
	
	void (FunType)(int x);
	
	void mian()
	{
		FunType f = end;
		f = middleware2(f);
		f = middleware1(f);
		int i = 1;
		f(i);
	}
	
	void End(int x)
	{
		//output ending...
	}
	
	FunType middleware1(FunType f,int x)
	{
		return a;
	}
	
	FunType middleware2(FunType f,int x)
	{
		return b;
	}	
	void a(){
		//middleware1 logic
		//....
		f(x);
	}
	void b(){
		//middleware1 logic
		//....
		f(x);
	}	
	
2018-10-23
1 use(Fun<RequestDelegate,RequestDelegate> fun)中 RequestDelegate 设置方式 
	1 可以是lambda 表达式，
	2 可以是符合已经定义好的函数
2 为甚么需要传入一个  RequestDelegate 时 还要返回 RequestDelegate
	List<Fun<RequestDelegate,RequestDelegate>>  中  每个Fun<RequestDelegate,RequestDelegate> 的 next参数需要给定 形成一条方法链 chain of function
3 如何实现自定义 Middleware 并加入到 app中
	参考 UseMvc
4 自动自定义 Middleware  可传入定制的 lambda 表达式
	比如 EFcore 中的 
	            services.AddDbContext<ProjectContext>(options =>
					{
						options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")
						//, builder=>
						//builder.MigrationsAssembly(typeof(ProjectContext).Assembly.GetName().Name)
						,b => b.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name)
						
						);
					}
				);
		services.AddCustomizeMiddleWare(options =>
		{
			...
		}
	);

