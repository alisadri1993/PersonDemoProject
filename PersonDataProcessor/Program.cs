using EasyCaching.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonDataProcessor.DAL;
using PersonDataProcessor.DAL.Repositories;
using PersonDataProcessor.Service;
using PersonDataProcessor.Utility;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PersonDataProcessor
{
    public class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                CreateHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureServices((hostContext, services) =>
                {
                    Setting AppSetting = new Setting();
                    var sqlConnection = hostContext.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnectionString");
                    var x = hostContext.Configuration.GetSection("RedisConfig");
                    var section = hostContext.Configuration.GetSection(nameof(RedisConfig));
                    AppSetting.RedisConfiguration = section.Get<RedisConfig>();

                    services.Configure<Setting>(options =>
                    {
                        options.SqlConnectionString = sqlConnection;
                        options.RedisConfiguration = AppSetting.RedisConfiguration;
                    });
                    //services.AddScoped<IDbTransaction,SqlDb>(AppSetting);
                  
                    //services.AddScoped<IPersonRepository, PersonRepository>();


                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                    services.AddSingleton<IPersonService, PersonService>();

                    services.AddHostedService<Worker>();


                    //services.AddMemoryCache();

                    services.AddEasyCaching(options =>
                    {
                        options.UseRedis(config =>
                        {
                            config.DBConfig.Endpoints.Add(new ServerEndPoint(AppSetting.RedisConfiguration.HostAddress, AppSetting.RedisConfiguration.Port));
                        });
                    });
                });
    }
}
