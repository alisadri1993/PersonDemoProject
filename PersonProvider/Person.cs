using System;

namespace PersonProvider
{
    public class Person:IPersonEvent
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }

        public DateTime date => DateTime.Now;
    }
}
