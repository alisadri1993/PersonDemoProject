using MassTransit;
using System;
using System.Threading;

namespace PersonProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.Host(new Uri("rabbitmq://localhost"), c =>
                {
                    c.Username("guest");
                    c.Password("guest");
                });

            });

            bus.Start();

            Console.WriteLine("Publishing message");

            var sendEndpoint =  bus.GetSendEndpoint(new Uri("rabbitmq://localhost//PersonAddedQueue")).Result;



            var person = new Person {  name = "Ali", lastname = "sadri", age = 27 };

            while (true)
            {
                sendEndpoint.Send<Person>(person);
                Thread.Sleep(2000);
            }



            bus.Stop();

            Console.ReadLine();
        }
    }
}
