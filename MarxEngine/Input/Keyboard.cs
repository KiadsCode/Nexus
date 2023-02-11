#region Copyright
/*
 * Copyright KiadsCode
 * Nexus framework engine v1.3.6
 */
#endregion

namespace Nexus.Framework.Input
{
	/// <summary>
	///     Description of Keyboard.
	/// </summary>
	public static class Keyboard
    {
        public static KeyboardState GetState()
        {
            return new KeyboardState(0);
        }
    }
}