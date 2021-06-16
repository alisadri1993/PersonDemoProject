using EasyCaching.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonDataProcessor.DAL;
using PersonDataProcessor.Service;

namespace PersonDataProcessor.Utility
{
    public static class PersonDataServiceExtensions
    {
        public static void AddCustomeServices(this IServiceCollection services, HostBuilderContext hostContext)
        {
            Setting AppSetting = new Setting();
            var sqlConnection = hostContext.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnectionString");
            var x = hostContext.Configuration.GetSection("RedisConfig");
            AppSetting.RedisConfiguration = hostContext.Configuration.GetSection(nameof(RedisConfig)).Get<RedisConfig>();
            AppSetting.RabbitMqConfiguration = hostContext.Configuration.GetSection(nameof(RabbitMqConfig)).Get<RabbitMqConfig>();

            services.Configure<Setting>(options =>
            {
                options.SqlConnectionString = sqlConnection;
                options.RedisConfiguration = AppSetting.RedisConfiguration;
                options.RabbitMqConfiguration = AppSetting.RabbitMqConfiguration;
            });


            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPersonService, PersonService>();

            services.AddHostedService<Worker>();

            services.AddEasyCaching(options =>
            {
                options.UseRedis(config =>
                {
                    config.DBConfig.Endpoints.Add(new ServerEndPoint(AppSetting.RedisConfiguration.HostAddress, AppSetting.RedisConfiguration.Port));
                });
            });

            services.AddRabbitMqServices(hostContext,AppSetting.RabbitMqConfiguration);       }
    }
}
