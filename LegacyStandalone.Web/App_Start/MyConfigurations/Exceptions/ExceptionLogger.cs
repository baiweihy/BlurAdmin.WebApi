using System;
using System.Diagnostics;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace LegacyStandalone.Web.MyConfigurations.Exceptions
{
    public class MyExceptionLogger : ExceptionLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void Log(ExceptionLoggerContext context)
        {
#if DEBUG
            Trace.TraceError(context.ExceptionContext.Exception.ToString());
#endif
            LogException(context.ExceptionContext.Exception);
        }

        private void LogException(Exception ex)
        {
            if (ex != null)
            {
                LogException(ex.InnerException);
                Logger.Error(ex.ToString());
            }
        }
    }
}