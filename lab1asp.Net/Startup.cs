using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace lab1asp.Net
{
    public class Startup
    {
        //delegate
        public delegate Task RequestDelegate(HttpContext context);
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
 

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // RUN,USE, delegates
            

          /*   int x = 2;
             app.Use(async (context, next) =>
             {
                 x = x * 2;      // 2 * 2 = 4
                 await next.Invoke();    // גûחמג app.Run
                 x = x * 2;      // 8 * 2 = 16
                 await context.Response.WriteAsync($"Result: {x}");
             });

             app.Run(async (context) =>
             {
                 x = x * 2;  //  4 * 2 = 8
                 await Task.FromResult(0);
             });
*/

            //Map
        int cash = 300000;
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("To login enter card and type pin(in serach line: /login?pin=****) hint: pin is 1111");
            });
        });

        app.Map("/login", Login);
        app.Map("/details", Details);


        void Details(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                if (context.Request.Query.ContainsKey("option"))
                {
                    if (context.Request.Query["option"] == "1")
                    {
                        await context.Response.WriteAsync($" Your cash: {cash}");
                    }
                    else {
                        await context.Response.WriteAsync("Not correct input");
                    }
                }
            });
        }

    }


        private void Login(IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            if (context.Request.Query.ContainsKey("pin") && context.Request.Query["pin"] == "1111")
            {
                await context.Response.WriteAsync(" You are logged in! \n");
            }
            else
            {
                await context.Response.WriteAsync("Invalid pin! try again");
            }
        });
    }

        
        }
}

