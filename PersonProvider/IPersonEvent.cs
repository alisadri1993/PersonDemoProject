using System;
using System.Collections.Generic;
using System.Text;

namespace PersonProvider
{
    public interface IPersonEvent
    {
        public DateTime date { get; }
        public string name { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
    }
}
