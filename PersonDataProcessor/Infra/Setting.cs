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
        public RabbitMqConfig RabbitMqConfiguration { get; set; }
    }

    public class RedisConfig
    {
        public string HostAddress { get; set; }
        public int Port { get; set; }
    }
    public class RabbitMqConfig
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PrefetchCount { get; set; }
        public string PersonAddedReceiveEndpoint { get; set; }
    }
}
