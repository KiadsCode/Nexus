/*
 * Создано в SharpDevelop.
 * Пользователь: Acer
 * Дата: 01.02.2023
 * Время: 8:04
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */

namespace Nexus.Framework
{
	/// <summary>
	/// Description of 2D HitBox
	/// </summary>
	public class HitBox
	{
		public float X, Y;
		public int Width, Height;
		#region Properties
		public float Left
		{
			get
			{
				return X;
			}
		}
		public float Right
		{
			get
			{
				return X + Width * 2;
			}
		}
		public float Top
		{
			get
			{
				return Y;
			}
		}
		public float Bottom
		{
			get
			{
				return Y - Height * 2;
			}
		}
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
			HitBox other = obj as HitBox;
				if (other == null)
					return false;
						return this.Width == other.Width && this.Height == other.Height && this.X == other.X && this.Y == other.Y;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				hashCode += 1000000007 * Width.GetHashCode();
				hashCode += 1000000009 * Height.GetHashCode();
				hashCode += 1000000021 * X.GetHashCode();
				hashCode += 1000000033 * Y.GetHashCode();
			}
			return hashCode;
		}

		public static bool operator ==(HitBox lhs, HitBox rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}

		public static bool operator !=(HitBox lhs, HitBox rhs) {
			return !(lhs == rhs);
		}

		#endregion
		
	}
}
