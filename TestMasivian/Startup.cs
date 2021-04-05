using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCaching.Core.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using TestMasivian.Data;
using TestMasivian.Services;

namespace TestMasivian
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddEasyCaching(options =>
            {

                //use redis
                options.UseRedis(redisConfig =>
                {

                    redisConfig.DBConfig.Endpoints.Add(new ServerEndPoint("127.0.0.1", 6379));
                    redisConfig.DBConfig.AllowAdmin = true;
                    redisConfig.DBConfig.AbortOnConnectFail = false;
                    redisConfig.DBConfig.ConnectionTimeout = 5000;

                },
                    "roulette");
                
            });

            services.AddScoped<IRouletteData, RouletteData>();
            services.AddScoped<IRouletteService, RouletteService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}