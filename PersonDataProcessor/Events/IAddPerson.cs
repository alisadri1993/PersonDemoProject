using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.Events
{
    public interface IAddPerson
    {
        string id { get; set; }
        string name { get; set; }
        string lastname { get; set; }
        string age { get; set; }
    }
}
