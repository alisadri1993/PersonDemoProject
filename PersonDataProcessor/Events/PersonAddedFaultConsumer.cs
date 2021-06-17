﻿using Contract;
using MassTransit;
using Microsoft.Extensions.Logging;
using PersonDataProcessor.Utility.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.Events
{
    public class PersonAddedFaultConsumer : IConsumer<Fault<PersonData>>
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<PersonAddedConsumer> logger;

        public PersonAddedFaultConsumer(IServiceProvider serviceProvider,
                                        ILogger<PersonAddedConsumer> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }
        public async Task Consume(ConsumeContext<Fault<PersonData>> context)
        {
            foreach (var exception in context.Message.Exceptions)
            {
                if (exception is DomainException)
                {
                    logger.LogError($"Domain Exception Occurred! \n {exception}");
                }
                else
                {
                    logger.LogCritical($"Some Issue Occurred ! \n {exception}");
                }
            }

            await Task.CompletedTask;
        }
    }
}
