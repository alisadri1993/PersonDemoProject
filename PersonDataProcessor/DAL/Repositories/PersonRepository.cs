using Dapper;
using Dapper.Contrib.Extensions;
using PersonDataProcessor.Model;
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
        public PersonRepository(IDbTransaction transaction)
          : base(transaction)
        {
        }
        public Person CreatePerson(Person person)
        {
            try
            {
                person.id = Connection.ExecuteScalar<int>(
                    "INSERT INTO Persons(name, lastname,age) VALUES(@name,@lastname,@age); SELECT SCOPE_IDENTITY()",
                    param: new { name = person.name, lastname = person.lastname, age = person.age },
                    transaction: Transaction
                    );

                //person.Id = await Connection.InsertAsync(person);
                return person;
            }
            catch (Exception ex)
            {
                string m = ex.ToString();
                throw;
            }
        }

        public Person GetPersonById(int personId)
        {
            try
            {
                var person =  Connection.Query<Person>(
                              "SELECT * FROM Persons WHERE id = @id",
                              param: new { id = personId },
                              transaction: Transaction
                          ).FirstOrDefault();

                return person;
            }
            catch (Exception ex)
            {

                throw;
            }
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
