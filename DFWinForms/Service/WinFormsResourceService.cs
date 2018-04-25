using System;
using System.Collections.Generic;
using System.Drawing;
using ICSharpCode.Core;
using DFWinForms.Class;
namespace DFWinForms.Service
{
    public static class WinFormsResourceService
    {
        private static System.Collections.Generic.Dictionary<string, System.Drawing.Icon> iconCache;
        private static System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap> bitmapCache;
        static WinFormsResourceService()
        {
            WinFormsResourceService.iconCache = new System.Collections.Generic.Dictionary<string, System.Drawing.Icon>();
            WinFormsResourceService.bitmapCache = new System.Collections.Generic.Dictionary<string, System.Drawing.Bitmap>();
        }
        public static System.Drawing.Icon GetIcon(string name)
        {
            System.Drawing.Icon result;
            lock (WinFormsResourceService.iconCache)
            {
                System.Drawing.Icon icon;
                if (WinFormsResourceService.iconCache.TryGetValue(name, out icon))
                {
                    result = icon;
                }
                else
                {
                    object imageResource = ResourceService.GetImageResource(name);
                    if (imageResource == null)
                    {
                        result = null;
                    }
                    else
                    {
                        if (imageResource is System.Drawing.Icon)
                        {
                            icon = (imageResource as System.Drawing.Icon);
                        }
                        else
                        {
                            icon = WinFormsResourceService.BitmapToIcon((System.Drawing.Bitmap)imageResource);
                        }
                        WinFormsResourceService.iconCache[name] = icon;
                        result = icon;
                    }
                }
            }
            return result;
        }
        public static System.Drawing.Icon BitmapToIcon(System.Drawing.Bitmap bitmap)
        {
            System.IntPtr hicon = bitmap.GetHicon();
            System.Drawing.Icon result;
            try
            {
                using (System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(hicon))
                {
                    result = new System.Drawing.Icon(icon, icon.Width, icon.Height);
                }
            }
            finally
            {
                NativeMethods.DestroyIcon(hicon);
            }
            return result;
        }
        public static System.Drawing.Bitmap GetBitmap(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            System.Drawing.Bitmap result;
            lock (WinFormsResourceService.bitmapCache)
            {
                System.Drawing.Bitmap bitmap;
                if (WinFormsResourceService.bitmapCache.TryGetValue(name, out bitmap))
                {
                    result = bitmap;
                }
                else
                {
                    bitmap = (System.Drawing.Bitmap)ResourceService.GetImageResource(name);
                    if (bitmap != null)
                    {
                        WinFormsResourceService.bitmapCache[name] = bitmap;
                    }
                    result = bitmap;
                }
            }
            return result;
        }
    }
}
