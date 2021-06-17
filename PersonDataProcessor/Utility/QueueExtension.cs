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

namespace PersonDataProcessor.Utility
{
    public static class QueueExtension
    {
        public static IServiceCollection RegisterQueueServices(this IServiceCollection services
                                                            , HostBuilderContext context,
                                                              RabbitMqConfig queueSettings)
        {


            services.AddMassTransit(c =>
            {
                c.AddConsumer<PersonAddedConsumer>();
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
//        public static void AddRabbitMqServices(this IServiceCollection services, HostBuilderContext hostContext, RabbitMqConfig rabbitMqConfig)
//        {

//            services.AddMassTransit(x =>
//            {
//                x.AddConsumer<AddPersonConsumer>();
//                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
//                {
//                    //cfg.UseHealthCheck(provider);
//                    cfg.Host(new Uri($"rabbitmq://{rabbitMqConfig.Host}"), h =>
//                    {
//                        h.Username(rabbitMqConfig.Username);
//                        h.Password(rabbitMqConfig.Password);
//                    });
//                    cfg.ReceiveEndpoint(rabbitMqConfig.PersonAddedReceiveEndpoint, ep =>
//                    {
//                        //ep.ConfigureError(x=> 
//                        //{
//                        //    x.UseFilter(new GenerateFaultFilter());
//                        //});
//                        //ep.PrefetchCount = 3;
//                        //ep.UseMessageRetry(r =>
//                        //{
//                        //    r.Immediate(5);
//                        //    r.Handle<DomainException>(x => x.Message.Contains("Domain Exception"));
//                        //});
//                        ep.ConfigureConsumer<AddPersonConsumer>(provider);
//                    });
//                }));
//            });

//            services.AddMassTransitHostedService();
//            //var builder = new ContainerBuilder();
//            //builder.Register(c=> 
//            //{
//            //    return Bus.Factory.CreateUsingRabbitMq(sbc =>
//            //    {
//            //        sbc.Host(rabbitMqConfig.Host, "/", h =>
//            //        {
//            //            h.Username(rabbitMqConfig.Username);
//            //            h.Password(rabbitMqConfig.Password);
//            //        });
//            //    });
//            //})
//            //    .As<IBusControl>()
//            //    .As<IBus>()
//            //    .As<IPubli>
//            //    .As

//        }
//    }
//}