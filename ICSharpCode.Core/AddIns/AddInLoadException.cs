using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ICSharpCode.Core
{
    public class AddInLoadException:CoreException
    {
        public AddInLoadException()
            : base()
        {
        }

        public AddInLoadException(string message)
            : base(message)
        {
        }

        public AddInLoadException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AddInLoadException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
