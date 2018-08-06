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

		
