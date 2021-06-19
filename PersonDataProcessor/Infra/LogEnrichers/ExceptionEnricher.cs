using Serilog.Core;
using Serilog.Events;

namespace PersonDataProcessor.Utility.LogEnrichers
{
    public class ExceptionEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Exception != null)
            {
                //some actions
                propertyFactory.CreateProperty("ExceptionDetails", logEvent.Exception);
            }
        }
    }
}
