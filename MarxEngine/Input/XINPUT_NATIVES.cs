using System.Runtime.InteropServices;

namespace Nexus.Framework.Input
{
    internal class XINPUT_NATIVES
    {
        [DllImport("xinput1_3.dll", EntryPoint = "XInputGetState")]
        public static extern ErrorCodes GetState(PlayerIndex playerIndex, out XINPUT_STATE pState);

        [DllImport("xinput1_3.dll", EntryPoint = "XInputSetState")]
        public static extern ErrorCodes SetState(PlayerIndex playerIndex, ref XINPUT_VIBRATION pVibration);

        [DllImport("xinput1_3.dll", EntryPoint = "XInputGetCapabilities")]
        public static extern ErrorCodes GetCaps(PlayerIndex playerIndex, uint flags, out XINPUT_CAPABILITIES pCaps);
    }
}