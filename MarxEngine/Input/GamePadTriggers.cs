using System;
using System.Globalization;

namespace Nexus.Framework.Input
{
    public struct GamePadTriggers
    {
        public GamePadTriggers(float leftTrigger, float rightTrigger)
        {
            _left = leftTrigger;
            _right = rightTrigger;
            _left = Math.Min(_left, 1f);
            _left = Math.Max(_left, 0f);
            _right = Math.Min(_right, 1f);
            _right = Math.Max(_right, 0f);
        }

        public float Left => _left;

        public float Right => _right;

        public override bool Equals(object obj)
        {
            return obj != null && !(obj.GetType() != GetType()) && this == (GamePadTriggers)obj;
        }

        public override int GetHashCode()
        {
            return Helpers.SmartGetHashCode(this);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "{{Left:{0} Right:{1}}}", _left, _right);
        }

        public static bool operator ==(GamePadTriggers left, GamePadTriggers right)
        {
            return left._left == right._left && left._right == right._right;
        }

        public static bool operator !=(GamePadTriggers left, GamePadTriggers right)
        {
            return left._left != right._left || left._right != right._right;
        }

        internal float _left;

        internal float _right;
    }
}