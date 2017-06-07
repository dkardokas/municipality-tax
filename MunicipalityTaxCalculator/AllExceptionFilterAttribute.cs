using System.Diagnostics;
using System.Web.Http.Filters;

namespace MunicipalityTaxCalculator
{
    class AllExceptionFilterAttribute : ExceptionFilterAttribute
    {
        string _source = "Municipality Tax Calculator";
        string _log = "Application";
        string _event = "Exception";
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            
            if (!EventLog.SourceExists(_source))
                EventLog.CreateEventSource(_source, _log);

            EventLog.WriteEntry(_source, "Exception occured: " + exception.Message + " /n Stack Trace: " + exception.StackTrace, EventLogEntryType.Error);
        }
    }

}
