using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWord
{
    //Startup 类可以用来定义请求处理管道和配置应用程序需要的服务。
    public class Startup
    {
        public Startup()
        {
            //加载 AppSettings.json 文件，然后构建配置项Configuration
            //var builder = new ConfigurationBuilder().AddJsonFile("AppSettings.json");
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("AppSettings.json");
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        // ConfigureServices()方法用于定义应用程序所需要的服务，例如 ASP.NET Core MVC 、 Entity Framework Core 和 Identity 等等
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();//注册 MVC 服务
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //Configure() 用于定义请求管道中的中间件
        //中间件是一种装配到应用程序管道以处理请求和响应的组件。
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//中间件:使用開發環境拋下出异常详细信息
            }

            //app.UseWelcomePage();//中间件：使用歡迎頁面
            //UseDefaultFiles 中间件会检查传入的请求并检查它是否用于目录的根目录，以及是否有任何匹配的默认文件
            //app.UseDefaultFiles();//中间件
            //app.UseStaticFiles();//中间件：
            app.UseFileServer();//中间件：UseFileServer()=UseDefaultFiles()+UseStaticFiles()

            //MVC中間件
            //app.UseMvcWithDefaultRoute();//中间件：預設路由模式
            app.UseMvc(ConfigureRoute);//中间件：配置路由模式


            //Run() 方法不经常见，它是调用中间件的终端
            app.Run(async (context) =>
            {
                //throw new System.Exception("Throw Exception");
                //默认都会使用 context.Response.WriteAsync 中间件
                //await context.Response.WriteAsync("Hello World!\n 測試.net core");

                //从配置项中读取 message 并作为响应的内容


                var msg = Configuration["message"];
                await context.Response.WriteAsync(msg);
            });
        }

        private void ConfigureRoute(IRouteBuilder routeBuilder)
        {
            //throw new NotImplementedException();
            //配置路由
            //映射一个新的路由，它的名字是 Default，然后提供最重要的路由信息​​，路由模板
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }


    }
}
