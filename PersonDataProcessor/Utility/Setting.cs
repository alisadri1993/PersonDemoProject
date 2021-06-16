using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDataProcessor.Utility
{
    public class Setting
    {
        public string SqlConnectionString { get; set; }
        public RedisConfig RedisConfiguration { get; set; }
    }

    public class RedisConfig
    {
        public string HostAddress { get; set; }
        public int Port { get; set; }
    }
}
