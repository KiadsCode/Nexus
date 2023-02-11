using System;

namespace Nexus.Framework.Input
{
    /// <summary>
    ///     Description of MouseState.
    /// </summary>
    public struct MouseState : IEquatable<MouseState>
    {
        private readonly bool RightButton;
        private readonly bool MiddleButton;
        private readonly bool LeftButton;

        private static bool GetAsyncKeyState(int value)
        {
            return NativeMethods.GetAsyncKeyState(value);
        }

        // disable once UnusedParameter
        internal MouseState(int placeHolder)
        {
            LeftButton = GetAsyncKeyState((int)MouseButton.LeftButton);
            RightButton = GetAsyncKeyState((int)MouseButton.RightButton);
            MiddleButton = GetAsyncKeyState((int)MouseButton.MiddleButton);
        }

        public int X => NativeMethods.GetPositionX(0);
        public int Y => NativeMethods.GetPositionY(0);

        public bool IsButtonPressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.LeftButton:
                    return LeftButton;
                case MouseButton.RightButton:
                    return RightButton;
                case MouseButton.MiddleButton:
                    return MiddleButton;
            }

            return false;
        }

        public bool IsButtonRelease(MouseButton button)
        {
            return !IsButtonPressed(button);
        }

        #region Equals and GetHashCode implementation

        public override bool Equals(object obj)
        {
            return obj is MouseState && Equals((MouseState)obj);
        }

        public bool Equals(MouseState other)
        {
            return RightButton == other.RightButton && MiddleButton == other.MiddleButton &&
                   LeftButton == other.LeftButton;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            unchecked
            {
                hashCode += 1000000007 * RightButton.GetHashCode();
                hashCode += 1000000009 * MiddleButton.GetHashCode();
                hashCode += 1000000021 * LeftButton.GetHashCode();
            }

            return hashCode;
        }

        public static bool operator ==(MouseState lhs, MouseState rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(MouseState lhs, MouseState rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}