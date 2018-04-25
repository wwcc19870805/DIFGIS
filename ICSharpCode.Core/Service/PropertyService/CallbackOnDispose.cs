using System;
using System.Threading;
namespace ICSharpCode.Core
{
    internal class CallbackOnDispose : System.IDisposable
    {
        private System.Threading.ThreadStart callback;
        public CallbackOnDispose(System.Threading.ThreadStart callback)
        {
            if (callback == null)
            {
                throw new System.ArgumentNullException();
            }
            this.callback = callback;
        }
        public void Dispose()
        {
            System.Threading.ThreadStart threadStart = System.Threading.Interlocked.Exchange<System.Threading.ThreadStart>(ref this.callback, null);
            if (threadStart != null)
            {
                threadStart();
            }
        }
    }
}
