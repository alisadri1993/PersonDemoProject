using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PersonDataProcessor.DAL;
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
        private readonly IUnitOfWork unitOfWork;

        public Worker(ILogger<Worker> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", "");

                var person = await unitOfWork.PersonRepository.CreatePerson(new Model.Person { name = "ali", personId = Guid.NewGuid().ToString() });
                unitOfWork.commit();


                _logger.LogWarning("this is warning {person}", person.ToString());

                var person2 = await unitOfWork.PersonRepository.GetPersonById(person.Id);



                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
