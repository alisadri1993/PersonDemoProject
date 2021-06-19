using Serilog.Core;
using Serilog.Events;
using System;
using System.Reflection;

namespace PersonDataProcessor.Utility.LogEnrichers
{

    public class ApplicationDetailsEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var applicationAssembly = Assembly.GetEntryAssembly();
            var name = applicationAssembly.GetName().Name;
            var version = applicationAssembly.GetName().Version;

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ApplicationName", name));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ApplicationVersion", version));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("MachineName", Environment.MachineName));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("ProcessId", Environment.ProcessId));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("OSVersion", Environment.OSVersion));
        }
    }

}
