
using System;

namespace Nexus.Framework.Input
{
	/// <summary>
	///     Description of Mouse.
	/// </summary>
	public static class Mouse
    {
        public static MouseState GetState()
        {
            ButtonState[] buttons = new[]
            {
                KeyStateToButtonState((int)MouseButton.LeftButton),
                KeyStateToButtonState((int)MouseButton.RightButton),
                KeyStateToButtonState((int)MouseButton.MiddleButton)
            };
            
            return new MouseState(buttons[0], buttons[1], buttons[2]);
        }

        private static bool GetAsyncKeyState(int value)
        {
            return Convert.ToBoolean(NativeMethods.GetAsyncKeyState(value));
        }
        
        
        private static ButtonState KeyStateToButtonState(int value)
        {
            bool val = Convert.ToBoolean(NativeMethods.GetAsyncKeyState(value));
            if (val)
                return ButtonState.Pressed;
            return ButtonState.Released;
        }
        
        public static void SetPosition(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
        }
    }
}