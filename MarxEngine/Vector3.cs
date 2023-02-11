#region Copyright
/*
 * Copyright KiadsCode
 * Nexus framework engine v1.3.6
 */
#endregion

namespace Nexus.Framework
{
	/// <summary>
	///     Description of Vector2.
	/// </summary>
	public class Vector3
    {
        public float X, Y, Z;

        public static Vector3 Lerp(Vector3 a, Vector3 b, float c)
        {
            var result = Zero;
            result.X = MathHelper.Lerp(a.X, b.X, c);
            result.Y = MathHelper.Lerp(a.Y, b.Y, c);
            result.Z = MathHelper.Lerp(a.Z, b.Z, c);
            return result;
        }

        public static Vector3 Zero => new Vector3(0, 0, 0);

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        #region Equals and GetHashCode implementation

        public override bool Equals(object obj)
        {
            var other = obj as Vector3;
            if (other == null)
                return false;
            return Equals(X, other.X) && Equals(Y, other.Y) && Equals(Z, other.Z);
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            unchecked
            {
                hashCode += 1000000007 * X.GetHashCode();
                hashCode += 1000000009 * Y.GetHashCode();
                hashCode += 1000000021 * Z.GetHashCode();
            }

            return hashCode;
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return false;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return !(lhs == rhs);
        }

        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static Vector3 operator -(Vector3 lhs)
        {
            return new Vector3(-lhs.X, -lhs.Y, -lhs.Z);
        }

        #endregion
    }
}