using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonDataProcessor.DAL;
using PersonDataProcessor.Service;
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

        public Worker(ILogger<Worker> logger, IPersonService personService)
        {
            _logger = logger;
            this.personService = personService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", "");

                //var person = await unitOfWork.PersonRepository.CreatePerson(new Model.Person { name = "ali", personId = Guid.NewGuid().ToString() });
                //unitOfWork.commit();
                
                
                
                var person = await personService.SavePerson(new Model.Person { name = "ali", personId = Guid.NewGuid().ToString() });
                
                
                await personService.LoadPersonById(person.Id);




                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
