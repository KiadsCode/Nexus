namespace Nexus.Framework
{
	/// <summary>
	///     Description of 2D HitBox
	///     Allow to collide with objects
	/// </summary>
	public class HitBox
    {
        public float X, Y;
        public int Width, Height;

        #region Properties

        public float Left => X;
        public float Right => X + Width * 2;

        public float Top => Y;

        public float Bottom => Y - Height * 2;

        #endregion

        public HitBox(float x, float y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public bool Intersects(HitBox value)
        {
            return Left < value.Right &&
                   Right > value.Left &&
                   Top > value.Bottom &&
                   Bottom < value.Top;
        }

        #region Equals and GetHashCode implementation

        public override bool Equals(object obj)
        {
            var other = obj as HitBox;
            if (other == null)
                return false;
            return Width == other.Width && Height == other.Height && X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            unchecked
            {
                hashCode += 1000000007 * Width.GetHashCode();
                hashCode += 1000000009 * Height.GetHashCode();
                hashCode += 1000000021 * X.GetHashCode();
                hashCode += 1000000033 * Y.GetHashCode();
            }

            return hashCode;
        }

        public static bool operator ==(HitBox lhs, HitBox rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return false;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(HitBox lhs, HitBox rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}