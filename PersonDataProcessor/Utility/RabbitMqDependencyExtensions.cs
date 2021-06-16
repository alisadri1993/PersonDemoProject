using Autofac;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonDataProcessor.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.Utility
{
    public static class RabbitMqDependencyExtensions
    {
        public static void AddRabbitMqServices(this IServiceCollection services, HostBuilderContext hostContext, RabbitMqConfig rabbitMqConfig)
        {

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AddPersonConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    //cfg.UseHealthCheck(provider);
                    cfg.Host(new Uri($"rabbitmq://{rabbitMqConfig.Host}"), h =>
                    {
                        h.Username(rabbitMqConfig.Username);
                        h.Password(rabbitMqConfig.Password);
                    });
                    cfg.ReceiveEndpoint(rabbitMqConfig.PersonAddedReceiveEndpoint, ep =>
                    {
                        ep.PrefetchCount = 3;
                        //ep.UseMessageRetry(r => r.r.Interval(2, 100));
                        ep.ConfigureConsumer<AddPersonConsumer>(provider);
                    });
                }));
            });

            services.AddMassTransitHostedService();
            //var builder = new ContainerBuilder();
            //builder.Register(c=> 
            //{
            //    return Bus.Factory.CreateUsingRabbitMq(sbc =>
            //    {
            //        sbc.Host(rabbitMqConfig.Host, "/", h =>
            //        {
            //            h.Username(rabbitMqConfig.Username);
            //            h.Password(rabbitMqConfig.Password);
            //        });
            //    });
            //})
            //    .As<IBusControl>()
            //    .As<IBus>()
            //    .As<IPubli>
            //    .As

        }
    }
}