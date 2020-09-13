using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace lab1asp.Net
{
    public class Startup
    {
        //Delegate
        public delegate Task RequestDelegate(HttpContext context);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        private static int cash = 1000;

        private static void PIN(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await next.Invoke();
                await context.Response.WriteAsync($"PIN code: ****");
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync($"PIN code: 1111");
            });
        }

        private static void Balance(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync($"Balance: {cash}");
            });
        }

        private static void CashWithdraw(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                cash -= 150;
                await next.Invoke();            
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync($"Available:{cash}");
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            
            app.Map("/pin", PIN);
            app.Map("/cash balance", Balance);
            app.Map("/cas hwithdraw", CashWithdraw);

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("\nCash Machine\n\n/PIN - ****\n/balance -850tg\n/cashwithdraw - 150tg\n\n");
            });
           
           
        }

      


    }
}

