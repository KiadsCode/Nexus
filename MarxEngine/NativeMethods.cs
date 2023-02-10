using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Nexus.Framework
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPTStr)] String atom, [MarshalAs(UnmanagedType.LPTStr)] String title);
        
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern bool GetAsyncKeyState(int vkey);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT point);

        [DllImport("user32.dll")]
        public static extern float SetCursorPos(int x, int y);

        public static int GetPositionX(int windowPosX)
        {
            int pos = 0;
            POINT p;
            if (GetCursorPos(out p))
                pos = p.x;
            pos -= windowPosX;
            return pos;
        }
        public static int GetPositionY(int windowPosY)
        {
            int pos = 0;
            POINT p;
            if (GetCursorPos(out p))
                pos = p.y;
            pos -= windowPosY;
            return pos;
        }

        public struct POINT
        {
            public int x;
            public int y;
        }
    }
}
