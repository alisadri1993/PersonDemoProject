using Autofac;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonDataProcessor.Events;
using PersonDataProcessor.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.Utility.Extensions
{
    public static class QueueExtensions
    {
        public static IServiceCollection RegisterQueueServices(this IServiceCollection services
                                                            , HostBuilderContext context,
                                                              RabbitMqConfig queueSettings)
        {
            services.AddMassTransit(c =>
            {
                c.AddConsumer<PersonAddedConsumer>();
                c.AddConsumer<PersonAddedFaultConsumer>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(queueSettings.Host, h =>
                {
                    h.Username(queueSettings.Username);
                    h.Password(queueSettings.Password);
                });
            }));

            services.AddMassTransitHostedService();
            return services;
        }
    }
}