using MassTransit;
using PersonDataProcessor.Model;
using PersonDataProcessor.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PersonDataProcessor.Events
{
    public class AddPersonConsumer : IConsumer<Person>
    {
        
        public async  Task Consume(ConsumeContext<Person> context)
        {
            var personService = DependencyResolver.Current.GetService<IPersonService>();

            var person = context.Message;
             await personService.SavePersonAsync(null);
        }
    }
}
