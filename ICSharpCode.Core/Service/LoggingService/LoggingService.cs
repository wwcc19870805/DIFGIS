using System;
namespace ICSharpCode.Core
{
    public static class LoggingService
    {
        public static bool IsDebugEnabled
        {
            get
            {
                return LoggingServiceManager.LoggingService.IsDebugEnabled;
            }
        }
        public static bool IsInfoEnabled
        {
            get
            {
                return LoggingServiceManager.LoggingService.IsInfoEnabled;
            }
        }
        public static bool IsWarnEnabled
        {
            get
            {
                return LoggingServiceManager.LoggingService.IsWarnEnabled;
            }
        }
        public static bool IsErrorEnabled
        {
            get
            {
                return LoggingServiceManager.LoggingService.IsErrorEnabled;
            }
        }
        public static bool IsFatalEnabled
        {
            get
            {
                return LoggingServiceManager.LoggingService.IsFatalEnabled;
            }
        }
        public static void Debug(object message)
        {
            LoggingServiceManager.LoggingService.Debug(message);
        }
        public static void DebugFormatted(string format, params object[] args)
        {
            LoggingServiceManager.LoggingService.DebugFormatted(format, args);
        }
        public static void Info(object message)
        {
            LoggingServiceManager.LoggingService.Info(message);
        }
        public static void InfoFormatted(string format, params object[] args)
        {
            LoggingServiceManager.LoggingService.InfoFormatted(format, args);
        }
        public static void Warn(object message)
        {
            LoggingServiceManager.LoggingService.Warn(message);
        }
        public static void Warn(object message, System.Exception exception)
        {
            LoggingServiceManager.LoggingService.Warn(message, exception);
        }
        public static void WarnFormatted(string format, params object[] args)
        {
            LoggingServiceManager.LoggingService.WarnFormatted(format, args);
        }
        public static void Error(object message)
        {
            LoggingServiceManager.LoggingService.Error(message);
        }
        public static void Error(object message, System.Exception exception)
        {
            LoggingServiceManager.LoggingService.Error(message, exception);
        }
        public static void ErrorFormatted(string format, params object[] args)
        {
            LoggingServiceManager.LoggingService.ErrorFormatted(format, args);
        }
        public static void Fatal(object message)
        {
            LoggingServiceManager.LoggingService.Fatal(message);
        }
        public static void Fatal(object message, System.Exception exception)
        {
            LoggingServiceManager.LoggingService.Fatal(message, exception);
        }
        public static void FatalFormatted(string format, params object[] args)
        {
            LoggingServiceManager.LoggingService.FatalFormatted(format, args);
        }
    }
}
