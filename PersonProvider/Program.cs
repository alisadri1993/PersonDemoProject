using Contract;
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

            var sendEndpoint = bus.GetSendEndpoint(new Uri("rabbitmq://localhost//PersonAddedQueue")).Result;



            var person = new PersonData 
            {
                name = "Ali",
                lastname = "sadri",
                //age = 14
            };

            while (true)
            {
                person.age = new Random().Next(20);
                sendEndpoint.Send<PersonData>(person);
                Thread.Sleep(5000);
            }



            bus.Stop();

            Console.ReadLine();
        }
    }
}
