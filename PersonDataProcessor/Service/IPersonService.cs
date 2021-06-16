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
        Task<Person> SavePerson();
        Task<Person> LoadPersonById();
        Task<ICollection<Person>> LoadPersons();
    }
}
