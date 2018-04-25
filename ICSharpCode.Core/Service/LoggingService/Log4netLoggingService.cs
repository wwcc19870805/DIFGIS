using log4net;
using log4net.Config;
using System;
using System.IO;
namespace ICSharpCode.Core
{
    public class Log4netLoggingService : ILoggingService
    {
        private ILog log;
        public bool IsDebugEnabled
        {
            get
            {
                return this.log.IsDebugEnabled;
            }
        }
        public bool IsInfoEnabled
        {
            get
            {
                return this.log.IsInfoEnabled;
            }
        }
        public bool IsWarnEnabled
        {
            get
            {
                return this.log.IsWarnEnabled;
            }
        }
        public bool IsErrorEnabled
        {
            get
            {
                return this.log.IsErrorEnabled;
            }
        }
        public bool IsFatalEnabled
        {
            get
            {
                return this.log.IsFatalEnabled;
            }
        }
        public Log4netLoggingService()
        {
            XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            this.log = LogManager.GetLogger(typeof(Log4netLoggingService));
        }

        public void Debug(object message)
        {
            this.log.Debug(message);
        }
        public void DebugFormatted(string format, params object[] args)
        {
            this.log.DebugFormat(format, args);
        }
        public void Info(object message)
        {
            this.log.Info(message);
        }
        public void InfoFormatted(string format, params object[] args)
        {
            this.log.InfoFormat(format, args);
        }
        public void Warn(object message)
        {
            this.log.Warn(message);
        }
        public void Warn(object message, System.Exception exception)
        {
            this.log.Warn(message, exception);
        }
        public void WarnFormatted(string format, params object[] args)
        {
            this.log.WarnFormat(format, args);
        }
        public void Error(object message)
        {
            this.log.Error(message);
        }
        public void Error(object message, System.Exception exception)
        {
            this.log.Error(message, exception);
        }
        public void ErrorFormatted(string format, params object[] args)
        {
            this.log.ErrorFormat(format, args);
        }
        public void Fatal(object message)
        {
            this.log.Fatal(message);
        }
        public void Fatal(object message, System.Exception exception)
        {
            this.log.Fatal(message, exception);
        }
        public void FatalFormatted(string format, params object[] args)
        {
            this.log.FatalFormat(format, args);
        }
    }
}
