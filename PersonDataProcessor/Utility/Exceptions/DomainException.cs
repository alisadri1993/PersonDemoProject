using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.Utility.Exceptions
{
    public class DomainException : Exception
    {
        public int Code { get; set; }

        public DomainException(string message, int code) : base(message)
        {
            this.Code = code;
        }
    }
}
