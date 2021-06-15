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
        public async Task<Person> CreatePerson(Person person)
        {
            try
            {
                person.Id = await Connection.ExecuteScalarAsync<int>(
                    "INSERT INTO Persons(personId,name, lastname,age) VALUES(@personId,@name,@lastname,@age); SELECT SCOPE_IDENTITY()",
                    param: new { personId = person.personId, name = person.name, lastname = person.lastname, age = person.age },
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

        public async Task<Person> GetPersonById(int personId)
        {
            try
            {
                var person = await Connection.QueryAsync<Person>(
                              "SELECT * FROM Persons WHERE id = @id",
                              param: new { id = personId },
                              transaction: Transaction
                          );

                return person.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<ICollection<Person>> GetPersons()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemovePersonById(int personId)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePerson(int personId, Person person)
        {
            throw new NotImplementedException();
        }
    }
}
