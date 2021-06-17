using MassTransit;
using PersonProvider;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PersonConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("PersonAddedQueue", e =>
                {
                    e.Consumer<OrderSubmittedEventConsumer>();
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await busControl.StartAsync(source.Token);
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }
        }

        class OrderSubmittedEventConsumer :
            IConsumer<IPersonEvent>
        {
            public async Task Consume(ConsumeContext<IPersonEvent> context)
            {
                Console.WriteLine("Order Submitted: {0}", context.Message.age);
            }
        }
    }
}