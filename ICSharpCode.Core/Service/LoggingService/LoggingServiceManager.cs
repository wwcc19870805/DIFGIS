using System;
namespace ICSharpCode.Core
{
    public static class LoggingServiceManager
    {
        private static ILoggingService log = new Log4netLoggingService();
        public static ILoggingService LoggingService
        {
            get
            {
                return LoggingServiceManager.log;
            }
            set
            {
                LoggingServiceManager.log = value;
            }
        }
    }
}
