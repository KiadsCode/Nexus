/*
 * Сделано в SharpDevelop.
 * Пользователь: Acer
 * Дата: 25.01.2023
 * Время: 18:01
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;

namespace Nexus.Framework
{
	/// <summary>
	/// Description of Vector2.
	/// </summary>
	public class Vector3
	{
		public float X, Y, Z;
		
		public static Vector3 Lerp(Vector3 a, Vector3 b, float c)
		{
			Vector3 result = Zero;
			result.X = MathHelper.Lerp(a.X, b.X, c);
			result.Y = MathHelper.Lerp(a.Y, b.Y, c);
			result.Z = MathHelper.Lerp(a.Z, b.Z, c);
			return result;
		}
		
		public static Vector3 Zero
		{
			get
			{
				return new Vector3(0, 0, 0);
			}
		}
		
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}
		
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			Vector3 other = obj as Vector3;
				if (other == null)
					return false;
						return object.Equals(this.X, other.X) && object.Equals(this.Y, other.Y) && object.Equals(this.Z, other.Z);
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				hashCode += 1000000007 * X.GetHashCode();
				hashCode += 1000000009 * Y.GetHashCode();
				hashCode += 1000000021 * Z.GetHashCode();
			}
			return hashCode;
		}

		public static bool operator ==(Vector3 lhs, Vector3 rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Vector3 lhs, Vector3 rhs) {
			return !(lhs == rhs);
		}

		#endregion
	}
}
