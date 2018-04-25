using System;
using System.Runtime.Serialization;
namespace ICSharpCode.Core
{
    public class CoreException : System.Exception
    {
        public CoreException()
        {
        }
        public CoreException(string strMsg)
            : base(strMsg)
        {
        }
        public CoreException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
        protected CoreException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
