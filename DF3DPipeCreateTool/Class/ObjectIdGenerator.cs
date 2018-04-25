using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace DF3DPipeCreateTool.Class
{
    public static class ObjectIdGenerator
    {
        // Fields
        private static int _counter;
        private static readonly object _innerLock = new object();
        private static readonly byte[] _machineHash = GenerateHostHash();
        private static readonly byte[] _processId = BitConverter.GetBytes(GenerateProcessId());
        private static readonly DateTime Epoch = new DateTime(0x7b2, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // Methods
        public static byte[] Generate()
        {
            byte[] destinationArray = new byte[12];
            int destinationIndex = 0;
            Array.Copy(BitConverter.GetBytes(GenerateTime()), 0, destinationArray, destinationIndex, 4);
            destinationIndex += 4;
            Array.Copy(_machineHash, 0, destinationArray, destinationIndex, 3);
            destinationIndex += 3;
            Array.Copy(_processId, 0, destinationArray, destinationIndex, 2);
            destinationIndex += 2;
            Array.Copy(BitConverter.GetBytes(_counter), 0, destinationArray, destinationIndex, 3);
            destinationIndex += 3;
            return destinationArray;
        }

        private static int GenerateCounter()
        {
            lock (_innerLock)
            {
                if (_counter > 0x7f)
                {
                    _counter = -1;
                }
                return _counter++;
            }
        }

        private static byte[] GenerateHostHash()
        {
            using (MD5 md = MD5.Create())
            {
                string hostName = Dns.GetHostName();
                return md.ComputeHash(Encoding.Default.GetBytes(hostName));
            }
        }

        private static int GenerateProcessId()
        {
            return Process.GetCurrentProcess().Id;
        }

        private static int GenerateTime()
        {
            DateTime utcNow = DateTime.UtcNow;
            DateTime time2 = new DateTime(Epoch.Year, Epoch.Month, Epoch.Day, utcNow.Hour, utcNow.Minute, utcNow.Second, utcNow.Millisecond);
            TimeSpan span = (TimeSpan)(time2 - Epoch);
            return Convert.ToInt32(Math.Floor(span.TotalMilliseconds));
        }
    }
}
