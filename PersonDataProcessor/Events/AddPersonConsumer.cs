using MassTransit;
using PersonDataProcessor.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PersonDataProcessor.Events
{
    public class AddPersonConsumer : IConsumer<IAddPerson>
    {
        
        public async  Task Consume(ConsumeContext<IAddPerson> context)
        {
            var personService = DependencyResolver.Current.GetService<IPersonService>();

            IAddPerson person = context.Message;
             await personService.SavePersonAsync(null);
        }
    }
}
