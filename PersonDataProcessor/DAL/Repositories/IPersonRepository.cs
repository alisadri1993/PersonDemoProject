using PersonDataProcessor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
