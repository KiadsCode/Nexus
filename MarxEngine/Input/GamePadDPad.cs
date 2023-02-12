using System.Globalization;

namespace Nexus.Framework.Input
{
    public struct GamePadDPad
    {
        public GamePadDPad(ButtonState upValue, ButtonState downValue, ButtonState leftValue, ButtonState rightValue)
        {
            _up = upValue;
            _right = rightValue;
            _down = downValue;
            _left = leftValue;
        }

        public ButtonState Up => _up;

        public ButtonState Down => _down;

        public ButtonState Right => _right;

        public ButtonState Left => _left;

        public override bool Equals(object obj)
        {
            return obj != null && !(obj.GetType() != GetType()) && this == (GamePadDPad)obj;
        }

        public override int GetHashCode()
        {
            return Helpers.SmartGetHashCode(this);
        }

        public override string ToString()
        {
            var text = string.Empty;
            if (_up == ButtonState.Pressed) text = text + (text.Length != 0 ? " " : "") + "Up";
            if (_down == ButtonState.Pressed) text = text + (text.Length != 0 ? " " : "") + "Down";
            if (_left == ButtonState.Pressed) text = text + (text.Length != 0 ? " " : "") + "Left";
            if (_right == ButtonState.Pressed) text = text + (text.Length != 0 ? " " : "") + "Right";
            if (text.Length == 0) text = "None";
            return string.Format(CultureInfo.CurrentCulture, "{{DPad:{0}}}", text);
        }

        public static bool operator ==(GamePadDPad left, GamePadDPad right)
        {
            return left._up == right._up && left._down == right._down && left._left == right._left &&
                   left._right == right._right;
        }

        public static bool operator !=(GamePadDPad left, GamePadDPad right)
        {
            return left._up != right._up || left._down != right._down || left._left != right._left ||
                   left._right != right._right;
        }

        internal ButtonState _up;

        internal ButtonState _right;

        internal ButtonState _down;

        internal ButtonState _left;
    }
}