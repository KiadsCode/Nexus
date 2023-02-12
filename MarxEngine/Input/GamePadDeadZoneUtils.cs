using System;

namespace Nexus.Framework.Input
{
    internal static class GamePadDeadZoneUtils
    {
        internal static Vector2 ApplyLeftStickDeadZone(int x, int y, GamePadDeadZone deadZoneMode)
        {
            return ApplyStickDeadZone(x, y, deadZoneMode, 7849);
        }

        internal static Vector2 ApplyRightStickDeadZone(int x, int y, GamePadDeadZone deadZoneMode)
        {
            return ApplyStickDeadZone(x, y, deadZoneMode, 8689);
        }

        private static Vector2 ApplyStickDeadZone(int x, int y, GamePadDeadZone deadZoneMode, int deadZoneSize)
        {
            var result = Vector2.Zero;
            if (deadZoneMode == GamePadDeadZone.IndependentAxes)
            {
                result.X = ApplyLinearDeadZone(x, 32767f, deadZoneSize);
                result.Y = ApplyLinearDeadZone(y, 32767f, deadZoneSize);
            }
            else if (deadZoneMode == GamePadDeadZone.Circular)
            {
                var num = (float)Math.Sqrt(x * x + y * y);
                var num2 = ApplyLinearDeadZone(num, 32767f, deadZoneSize);
                var num3 = num2 > 0f ? num2 / num : 0f;
                result.X = MathHelper.Clamp(x * num3, -1f, 1f);
                result.Y = MathHelper.Clamp(y * num3, -1f, 1f);
            }
            else
            {
                result.X = ApplyLinearDeadZone(x, 32767f, 0f);
                result.Y = ApplyLinearDeadZone(y, 32767f, 0f);
            }

            return result;
        }

        internal static float ApplyTriggerDeadZone(int value, GamePadDeadZone deadZoneMode)
        {
            if (deadZoneMode == GamePadDeadZone.None) return ApplyLinearDeadZone(value, 255f, 0f);
            return ApplyLinearDeadZone(value, 255f, 30f);
        }

        private static float ApplyLinearDeadZone(float value, float maxValue, float deadZoneSize)
        {
            if (value < -deadZoneSize)
            {
                value += deadZoneSize;
            }
            else
            {
                if (value <= deadZoneSize) return 0f;
                value -= deadZoneSize;
            }

            var value2 = value / (maxValue - deadZoneSize);
            return MathHelper.Clamp(value2, -1f, 1f);
        }

        private const int LeftStickDeadZoneSize = 7849;

        private const int RightStickDeadZoneSize = 8689;

        private const int TriggerDeadZoneSize = 30;
    }
}