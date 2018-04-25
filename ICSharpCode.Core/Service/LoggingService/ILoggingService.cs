using System;
namespace ICSharpCode.Core
{
    public interface ILoggingService
    {
        bool IsDebugEnabled
        {
            get;
        }
        bool IsInfoEnabled
        {
            get;
        }
        bool IsWarnEnabled
        {
            get;
        }
        bool IsErrorEnabled
        {
            get;
        }
        bool IsFatalEnabled
        {
            get;
        }
        void Debug(object message);
        void DebugFormatted(string format, params object[] args);
        void Info(object message);
        void InfoFormatted(string format, params object[] args);
        void Warn(object message);
        void Warn(object message, System.Exception exception);
        void WarnFormatted(string format, params object[] args);
        void Error(object message);
        void Error(object message, System.Exception exception);
        void ErrorFormatted(string format, params object[] args);
        void Fatal(object message);
        void Fatal(object message, System.Exception exception);
        void FatalFormatted(string format, params object[] args);
    }
}
