using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.Model
{
    [Table("dbo.Persons")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string personId { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public int age { get; set; }
    }
}
