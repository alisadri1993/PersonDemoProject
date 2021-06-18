using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PersonDataProcessor.Model;
using PersonDataProcessor.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.DAL.Repositories
{
    public class PersonRepository : RepositoryBase, IPersonRepository
    {
        private readonly ILogger<PersonRepository> logger;
        public PersonRepository(IDbTransaction transaction)
          : base(transaction)
        {
            logger = ServiceLocator.Current.GetInstance<ILogger<PersonRepository>>();
        }
        public Person CreatePerson(Person person)
        {
            logger.LogTrace($"start methode {nameof(CreatePerson)} in class {nameof(PersonRepository)}");

            person.id = Connection.ExecuteScalar<int>(
                "INSERT INTO Persons(name, lastname,age) VALUES(@name,@lastname,@age); SELECT SCOPE_IDENTITY()",
                param: new { name = person.name, lastname = person.lastname, age = person.age },
                transaction: Transaction
                );

            logger.LogTrace($"end methode {nameof(CreatePerson)} in class {nameof(PersonRepository)}");

            return person;

        }

        public Person GetPersonById(int personId)
        {
            var person = Connection.Query<Person>(
                                "SELECT * FROM Persons WHERE id = @id",
                                param: new { id = personId },
                                transaction: Transaction
                            ).FirstOrDefault();

            return person;
        }

        public ICollection<Person> GetPersons()
        {
            throw new NotImplementedException();
        }

        public bool RemovePersonById(int personId)
        {
            throw new NotImplementedException();
        }

        public Person UpdatePerson(int personId, Person person)
        {
            throw new NotImplementedException();
        }
    }
}
