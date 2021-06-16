using EasyCaching.Core;
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
        private readonly ILogger<PersonService> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEasyCachingProvider cachingProvider;

        public PersonService(ILogger<PersonService> logger, IUnitOfWork unitOfWork, IEasyCachingProvider cachingProvider)
        {
            this._logger = logger;
            this.unitOfWork = unitOfWork;
            this.cachingProvider = cachingProvider;
        }
        public async Task<Person> LoadPersonById(int personId)
        {

            Person person = (await cachingProvider.GetAsync<Person>(nameof(Person) + "_" + personId)).Value;

            if (person is null)
            {
                person = await unitOfWork.PersonRepository
                                         .GetPersonById(personId);
            }

            return person;

        }

        public Task<ICollection<Person>> LoadPersons()
        {
            throw new NotImplementedException();
        }

        public async Task<Person> SavePerson(Person person)
        {
            try
            {
                _logger.LogWarning("this is warning {person}", person.ToString());

                var addedperson = await unitOfWork.PersonRepository.CreatePerson(person);
                unitOfWork.commit();
                cachingProvider.Set<Person>(nameof(Person) + "_" + addedperson.Id, person, TimeSpan.FromMinutes(1));

                return addedperson;
            }
            catch (Exception ex)
            {

                throw;
            }

            return null;
        }
    }
}
