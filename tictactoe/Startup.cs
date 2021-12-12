using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using ILogger = Serilog.ILogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tictactoe.Repository.Contract;
using tictactoe.Repository;
using tictactoe.Service.Contract;
using tictactoe.Service;
using tictactoe.Data.Database;
using Microsoft.EntityFrameworkCore;
using tictactoe.Configuration;

namespace tictactoe
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            using var logger = new LoggerConfiguration()
               .ReadFrom.Configuration(Configuration)
               .CreateLogger();



            logger.Information("Starting ConfigureServices...");


            services.AddSingleton(typeof(ILogger), logger);
            ConfigureEventServices(services, logger);
            ConfigureRepositories(services, logger);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "tictactoe", Version = "v1" });
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            DatabaseConfiguration dConfig = new DatabaseConfiguration(Configuration);

            services.AddDbContext<DatabaseContext>(opt => {
                opt.UseMySQL(dConfig.MySQLConnectString());

            });
        }
        private static void ConfigureRepositories(IServiceCollection services, ILogger logger)
        {

            logger.Information("Configuring Repositories ...");
            services.AddScoped<IGameDbContext, DatabaseContext>();
            services.AddTransient<IGameRepository, GameRepository>();
           

        }
        public void ConfigureEventServices(IServiceCollection services, ILogger logger)
        {
            logger.Information("Configuring Services ...");
            services.AddTransient<IGameService,GameService>();
            
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "tictactoe v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
