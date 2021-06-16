using PersonDataProcessor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.Service
{
    public interface IPersonService
    {
        Task<Person> SavePersonAsync(Person person);
        Task<Person> LoadPersonByIdAsync(int personId);
        Task<ICollection<Person>> LoadPersonsAsync();
    }
}
