using System;
using System.Runtime.InteropServices;

namespace Nexus.Framework
{
    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        public static extern IntPtr FindWindow([MarshalAs(UnmanagedType.LPTStr)] string atom,
            [MarshalAs(UnmanagedType.LPTStr)] string title);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern short GetAsyncKeyState(int vkey);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT point);

        [DllImport("user32.dll")]
        public static extern float SetCursorPos(int x, int y);

        public static int GetPositionX(int windowPosX)
        {
            var pos = 0;
            POINT p;
            if (GetCursorPos(out p))
                pos = p.x;
            pos -= windowPosX;
            return pos;
        }

        public static int GetPositionY(int windowPosY)
        {
            var pos = 0;
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