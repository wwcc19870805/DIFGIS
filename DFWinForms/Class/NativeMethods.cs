using System;
using System.Runtime.InteropServices;
namespace DFWinForms.Class
{
    public static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool DestroyIcon(System.IntPtr hIcon);
    }
}
