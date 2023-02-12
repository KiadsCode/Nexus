using System;

namespace Nexus.Framework.Input
{
    /// <summary>
    ///     Description of MouseState.
    /// </summary>
    public struct MouseState
    {
        private readonly ButtonState _rightButton;
        private readonly ButtonState _middleButton;
        private readonly ButtonState _leftButton;

        // disable once UnusedParameter
        public MouseState(ButtonState leftButton, ButtonState rightButton, ButtonState middleButton)
        {
            _leftButton = leftButton;
            _rightButton = rightButton;
            _middleButton = middleButton;
        }

        public ButtonState LeftButton
        {
            get
            {
                return _leftButton;
            }
        }
        public ButtonState RightButton
        {
            get
            {
                return _rightButton;
            }
        }
        public ButtonState MiddleButton
        {
            get
            {
                return _middleButton;
            }
        }
        
        public int X => NativeMethods.GetPositionX(0);
        public int Y => NativeMethods.GetPositionY(0);
    }
}