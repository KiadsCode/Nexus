using System.Runtime.InteropServices;

namespace Nexus.Framework.Input
{
    public static class Helpers
    {
        public static unsafe int SmartGetHashCode(object obj)
        {
            var gchandle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            int result;
            try
            {
                var num = Marshal.SizeOf(obj);
                var num2 = 0;
                var num3 = 0;
                var ptr = (int*)gchandle.AddrOfPinnedObject().ToPointer();
                while (num2 + 4 <= num)
                {
                    num3 ^= *ptr;
                    num2 += 4;
                    ptr++;
                }

                result = num3 == 0 ? int.MaxValue : num3;
            }
            finally
            {
                gchandle.Free();
            }

            return result;
        }
    }
}