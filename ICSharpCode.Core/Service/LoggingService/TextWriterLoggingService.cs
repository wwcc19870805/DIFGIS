using System;
using System.IO;
namespace ICSharpCode.Core
{
    public class TextWriterLoggingService : ILoggingService
    {
        private readonly System.IO.TextWriter writer;
        public bool IsDebugEnabled
        {
            get;
            set;
        }
        public bool IsInfoEnabled
        {
            get;
            set;
        }
        public bool IsWarnEnabled
        {
            get;
            set;
        }
        public bool IsErrorEnabled
        {
            get;
            set;
        }
        public bool IsFatalEnabled
        {
            get;
            set;
        }
        public TextWriterLoggingService(System.IO.TextWriter writer)
        {
            if (writer == null)
            {
                throw new System.ArgumentNullException("writer");
            }
            this.writer = writer;
            this.IsFatalEnabled = true;
            this.IsErrorEnabled = true;
            this.IsWarnEnabled = true;
            this.IsInfoEnabled = true;
            this.IsDebugEnabled = true;
        }
        private void Write(object message, System.Exception exception)
        {
            if (message != null)
            {
                this.writer.WriteLine(message.ToString());
            }
            if (exception != null)
            {
                this.writer.WriteLine(exception.ToString());
            }
        }
        public void Debug(object message)
        {
            if (this.IsDebugEnabled)
            {
                this.Write(message, null);
            }
        }
        public void DebugFormatted(string format, params object[] args)
        {
            this.Debug(string.Format(format, args));
        }
        public void Info(object message)
        {
            if (this.IsInfoEnabled)
            {
                this.Write(message, null);
            }
        }
        public void InfoFormatted(string format, params object[] args)
        {
            this.Info(string.Format(format, args));
        }
        public void Warn(object message)
        {
            this.Warn(message, null);
        }
        public void Warn(object message, System.Exception exception)
        {
            if (this.IsWarnEnabled)
            {
                this.Write(message, exception);
            }
        }
        public void WarnFormatted(string format, params object[] args)
        {
            this.Warn(string.Format(format, args));
        }
        public void Error(object message)
        {
            this.Error(message, null);
        }
        public void Error(object message, System.Exception exception)
        {
            if (this.IsErrorEnabled)
            {
                this.Write(message, exception);
            }
        }
        public void ErrorFormatted(string format, params object[] args)
        {
            this.Error(string.Format(format, args));
        }
        public void Fatal(object message)
        {
            this.Fatal(message, null);
        }
        public void Fatal(object message, System.Exception exception)
        {
            if (this.IsFatalEnabled)
            {
                this.Write(message, exception);
            }
        }
        public void FatalFormatted(string format, params object[] args)
        {
            this.Fatal(string.Format(format, args));
        }
    }
}
