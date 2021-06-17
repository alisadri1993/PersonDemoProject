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
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _busControl;
        private readonly IPersonService personService;
        private readonly RabbitMqConfig rabbitMqConfig;

        public Worker(IServiceProvider serviceProvider, ILogger<Worker> logger, IBusControl busControl, IPersonService personService, IOptions<Setting> setting)
        {
            this.serviceProvider = serviceProvider;
            _logger = logger;
            this._busControl = busControl;
            this.personService = personService;
            this.rabbitMqConfig = setting.Value.RabbitMqConfiguration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            var personAddedEventHandler = _busControl.ConnectReceiveEndpoint(rabbitMqConfig.PersonAddedReceiveEndpoint, x =>
            {
                x.Consumer<PersonAddedConsumer>();
            });


            await personAddedEventHandler.Ready;


            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", "");


                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
