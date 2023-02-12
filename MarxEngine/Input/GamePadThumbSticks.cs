using System.Globalization;

namespace Nexus.Framework.Input
{
    public struct GamePadThumbSticks
    {
        public GamePadThumbSticks(Vector2 leftThumbstick, Vector2 rightThumbstick)
        {
            _left = leftThumbstick;
            _right = rightThumbstick;
            _left = Vector2.Min(_left, new Vector2(1f, 1f));
            _left = Vector2.Max(_left, -new Vector2(1f, 1f));
            _right = Vector2.Min(_right, new Vector2(1f, 1f));
            _right = Vector2.Max(_right, -new Vector2(1f, 1f));
        }

        public Vector2 Left => _left;

        public Vector2 Right => _right;

        public override bool Equals(object obj)
        {
            return obj != null && !(obj.GetType() != GetType()) && this == (GamePadThumbSticks)obj;
        }

        public override int GetHashCode()
        {
            return Helpers.SmartGetHashCode(this);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Left:{0} Right:{1}}}", _left, _right);
        }

        public static bool operator ==(GamePadThumbSticks left, GamePadThumbSticks right)
        {
            return left._left == right._left && left._right == right._right;
        }

        public static bool operator !=(GamePadThumbSticks left, GamePadThumbSticks right)
        {
            return left._left != right._left || left._right != right._right;
        }

        internal Vector2 _left;

        internal Vector2 _right;
    }
}