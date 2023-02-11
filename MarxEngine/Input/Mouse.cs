
namespace Nexus.Framework.Input
{
	/// <summary>
	///     Description of Mouse.
	/// </summary>
	public static class Mouse
    {
        public static MouseState GetState()
        {
            return new MouseState(0);
        }

        public static void SetPosition(int x, int y)
        {
            NativeMethods.SetCursorPos(x, y);
        }
    }
}