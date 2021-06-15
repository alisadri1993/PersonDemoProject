using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonDataProcessor.DAL;
using PersonDataProcessor.DAL.Repositories;
using PersonDataProcessor.Utility;
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


            //IConfiguration configuration = new ConfigurationBuilder()
            //  .AddJsonFile("appsettings.json", true, true)
            //  .Build();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    Setting AppSetting = new Setting();
                    var sqlConnection = hostContext.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnectionString");


                    services.Configure<Setting>(options=> 
                    {
                        options.SqlConnectionString = sqlConnection;
                        options.RedisConnectionString = sqlConnection;
                    });
                    //services.AddScoped<IDbTransaction,SqlDb>(AppSetting);
                    services.AddHostedService<Worker>();
                    //services.AddScoped<IPersonRepository, PersonRepository>();
                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                });
    }
}
