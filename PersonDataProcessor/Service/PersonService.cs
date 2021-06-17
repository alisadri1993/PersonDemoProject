using AutoMapper;
using Contract;
using EasyCaching.Core;
using Microsoft.Extensions.Logging;
using PersonDataProcessor.DAL;
using PersonDataProcessor.Model;
using PersonDataProcessor.Utility.Exceptions;
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
        private readonly IMapper mapper;

        public PersonService(ILogger<PersonService> logger,
                             IUnitOfWork unitOfWork,
                             IEasyCachingProvider cachingProvider,
                             IMapper mapper)
        {
            this._logger = logger;
            this.unitOfWork = unitOfWork;
            this.cachingProvider = cachingProvider;
            this.mapper = mapper;
        }
     
        public PersonData LoadPersonById(int personId)
        {
            Person person = cachingProvider.Get<Person>(nameof(Person) + "_" + personId).Value;
            if (person is null)
            {
                person =  unitOfWork.PersonRepository
                                         .GetPersonById(personId);
            }
            return mapper.Map<PersonData>(person);
        }
      
        public PersonData SavePerson(PersonData personDto)
        {

            Person person = mapper.Map<Person>(personDto);
            _logger.LogWarning(nameof(SavePerson) + " started {person}", person.ToString());

            if (person.age < 13)
                throw new DomainException("امکان افزودن افراد زیر 13 سال وجود ندارد", ExceptionCode.IvalidPersonAgeRange);

            var addedperson =  unitOfWork.PersonRepository.CreatePerson(person);
            unitOfWork.commit();
            cachingProvider.Set<Person>(nameof(Person) + "_" + addedperson.id, person, TimeSpan.FromMinutes(1));
            return mapper.Map<PersonData>(addedperson);
        }

        public ICollection<PersonData> LoadPersons()
        {
            throw new NotImplementedException();
        }
    }
}
