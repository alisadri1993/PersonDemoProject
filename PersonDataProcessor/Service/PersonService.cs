using Microsoft.Extensions.Logging;
using PersonDataProcessor.DAL;
using PersonDataProcessor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.Service
{
    public class PersonService : IPersonService
    {
        private readonly ILogger<PersonService> logger;
        private readonly IUnitOfWork unitOfWork;

        public PersonService(ILogger<PersonService> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }
        public Task<Person> LoadPersonById()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Person>> LoadPersons()
        {
            throw new NotImplementedException();
        }

        public Task<Person> SavePerson()
        {
            throw new NotImplementedException();
        }
    }
}
