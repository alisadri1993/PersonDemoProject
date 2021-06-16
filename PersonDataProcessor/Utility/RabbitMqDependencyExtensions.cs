using Autofac;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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