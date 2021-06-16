using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PersonDataProcessor.DAL;
using PersonDataProcessor.Events;
using PersonDataProcessor.Service;
using PersonDataProcessor.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PersonDataProcessor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IPersonService personService;
        private readonly RabbitMqConfig rabbitMqConfig;

        public Worker(ILogger<Worker> logger, IPersonService personService, IOptions<Setting> setting )
        {
            _logger = logger;
            this.personService = personService;
            this.rabbitMqConfig = setting.Value.RabbitMqConfiguration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {



            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri($"rabbitmq://{rabbitMqConfig.Host}/"), h =>
                {
                    h.Username(rabbitMqConfig.Username);
                    h.Password(rabbitMqConfig.Password);
                });

                cfg.ReceiveEndpoint( rabbitMqConfig.PersonAddedReceiveEndpoint, e =>
                {
                    e.Consumer<AddPersonConsumer>();
                });
            });

            await busControl.StartAsync();



            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", "");

                //var person = await unitOfWork.PersonRepository.CreatePerson(new Model.Person { name = "ali", personId = Guid.NewGuid().ToString() });
                //unitOfWork.commit();
                
                
                
                var person = await personService.SavePersonAsync(new Model.Person { name = "ali", personId = Guid.NewGuid().ToString() });
                
                
                await personService.LoadPersonByIdAsync(person.Id);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
