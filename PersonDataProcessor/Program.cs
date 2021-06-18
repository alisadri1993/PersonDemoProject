using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using PersonDataProcessor.Utility.Extensions;
using PersonDataProcessor.Utility.LogEnrichers;
using Serilog;

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
                    .Enrich.With<ExceptionEnricher>()
                    .Enrich.With<ApplicationDetailsEnricher>()
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
            .UseWindowsService()
            .UseSerilog()
             .ConfigureServices((hostContext, services) =>
             {
                 services.AddCustomeServices(hostContext);
             });
    }
}
