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
        Task<Person> CreatePerson(Person person);
        Task<Person> UpdatePerson(int personId, Person person);
        Task<Person> GetPersonById(int personId);
        Task<ICollection<Person>> GetPersons();
        Task<bool> RemovePersonById(int personId);
    }
}
