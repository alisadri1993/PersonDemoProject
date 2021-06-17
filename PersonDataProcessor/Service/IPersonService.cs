using Contract;
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
        PersonData SavePerson(PersonData person);
        PersonData LoadPersonById(int personId);
        ICollection<PersonData> LoadPersons();
    }
}
