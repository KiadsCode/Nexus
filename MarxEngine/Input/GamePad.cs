using System;
using System.Diagnostics;

namespace Nexus.Framework.Input
{
    public static class GamePad
    {
        public static GamePadState GetState(PlayerIndex playerIndex)
        {
            return GetState(playerIndex, GamePadDeadZone.IndependentAxes);
        }

        public static GamePadState GetState(PlayerIndex playerIndex, GamePadDeadZone deadZoneMode)
        {
            var xinput_STATE = default(XINPUT_STATE);
            ErrorCodes errorCodes;
            if (ThrottleDisconnectedRetries(playerIndex))
            {
                errorCodes = ErrorCodes.NotConnected;
            }
            else
            {
                errorCodes = XINPUT_NATIVES.GetState(playerIndex, out xinput_STATE);
                ResetThrottleState(playerIndex, errorCodes);
            }

            if (errorCodes != ErrorCodes.Success && errorCodes != ErrorCodes.NotConnected)
                throw new InvalidOperationException("InvalidController");
            return new GamePadState(ref xinput_STATE, errorCodes, deadZoneMode);
        }

        public static GamePadCapabilities GetCapabilities(PlayerIndex playerIndex)
        {
            var xinput_CAPABILITIES = default(XINPUT_CAPABILITIES);
            ErrorCodes errorCodes;
            if (ThrottleDisconnectedRetries(playerIndex))
            {
                errorCodes = ErrorCodes.NotConnected;
            }
            else
            {
                errorCodes = XINPUT_NATIVES.GetCaps(playerIndex, 1U, out xinput_CAPABILITIES);
                ResetThrottleState(playerIndex, errorCodes);
            }

            if (errorCodes != ErrorCodes.Success && errorCodes != ErrorCodes.NotConnected)
                throw new InvalidOperationException("InvalidController");
            return new GamePadCapabilities(ref xinput_CAPABILITIES, errorCodes);
        }

        public static bool SetVibration(PlayerIndex playerIndex, float leftMotor, float rightMotor)
        {
            XINPUT_VIBRATION xinput_VIBRATION;
            xinput_VIBRATION.LeftMotorSpeed = (short)(leftMotor * 65535f);
            xinput_VIBRATION.RightMotorSpeed = (short)(rightMotor * 65535f);
            ErrorCodes errorCodes;
            if (ThrottleDisconnectedRetries(playerIndex))
            {
                errorCodes = ErrorCodes.NotConnected;
            }
            else
            {
                errorCodes = XINPUT_NATIVES.SetState(playerIndex, ref xinput_VIBRATION);
                ResetThrottleState(playerIndex, errorCodes);
            }

            if (errorCodes == ErrorCodes.Success) return true;
            if (errorCodes != ErrorCodes.Success && errorCodes != ErrorCodes.NotConnected &&
                errorCodes != ErrorCodes.Busy) throw new InvalidOperationException("InvalidController");
            return false;
        }

        private static bool ThrottleDisconnectedRetries(PlayerIndex playerIndex)
        {
            if (playerIndex < PlayerIndex.One || playerIndex > PlayerIndex.Four) return false;
            if (!_disconnected[(int)playerIndex]) return false;
            var timestamp = Stopwatch.GetTimestamp();
            for (var i = 0; i < 4; i++)
                if (_disconnected[i])
                {
                    var num = timestamp - _lastReadTime[i];
                    var num2 = Stopwatch.Frequency;
                    if (i != (int)playerIndex) num2 /= 4L;
                    if (num >= 0L && num <= num2) return true;
                }

            return false;
        }

        private static void ResetThrottleState(PlayerIndex playerIndex, ErrorCodes result)
        {
            if (playerIndex < PlayerIndex.One || playerIndex > PlayerIndex.Four) return;
            if (result == ErrorCodes.NotConnected)
            {
                _disconnected[(int)playerIndex] = true;
                _lastReadTime[(int)playerIndex] = Stopwatch.GetTimestamp();
                return;
            }

            _disconnected[(int)playerIndex] = false;
        }

        internal const string XinputNativeDll = "xinput1_3.dll";

        private static readonly bool[] _disconnected = new bool[4];

        private static readonly long[] _lastReadTime = new long[4];
    }
}