using Contract;
using MassTransit;
using Microsoft.Extensions.Logging;
using PersonDataProcessor.Model;
using PersonDataProcessor.Service;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PersonDataProcessor.Events
{
    public class PersonAddedConsumer : IConsumer<PersonData>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IPersonService personService;
        private readonly ILogger<PersonAddedConsumer> logger;

        public PersonAddedConsumer(IServiceProvider serviceProvider,
                                   IPersonService personService,
                                   ILogger<PersonAddedConsumer> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
            this.personService = personService;
        }

        public async Task Consume(ConsumeContext<PersonData> context)
        {
            logger.LogInformation($"person data recieaved to consumer ==> {context.Message}");
            PersonData personData = context.Message;
            personData = personService.SavePerson(personData);
            logger.LogInformation($"person data processed end  in consumer class ==> {personData.ToString()}");

            await Task.CompletedTask;
        }
    }
}
