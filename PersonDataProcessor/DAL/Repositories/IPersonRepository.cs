using PersonDataProcessor.Model;
using System.Collections.Generic;

namespace PersonDataProcessor.DAL.Repositories
{
    public interface IPersonRepository
    {
        Person CreatePerson(Person person);
        Person UpdatePerson(int personId, Person person);
        Person GetPersonById(int personId);
        ICollection<Person> GetPersons();
        bool RemovePersonById(int personId);
    }
}
