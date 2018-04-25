using System;
using System.Runtime.Serialization;
namespace ICSharpCode.Core
{
    [System.Serializable]
    public class ResourceNotFoundException : CoreException
    {
        public ResourceNotFoundException(string resource)
            : base("Resource not found : " + resource)
        {
        }
        public ResourceNotFoundException()
        {
        }
        public ResourceNotFoundException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
        protected ResourceNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
