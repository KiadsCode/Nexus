/*
 * Создано в SharpDevelop.
 * Пользователь: Acer
 * Дата: 07.02.2023
 * Время: 14:19
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace Nexus.Framework.Input
{
	/// <summary>
	/// Description of KeyboardState.
	/// </summary>
	public struct KeyboardState : IEquatable<KeyboardState>
	{
		private bool A;
		private bool B;
		private bool C;
		private bool D;
		private bool E;
		private bool F;
		private bool G;
		private bool H;
		private bool I;
		private bool J;
		private bool K;
		private bool L;
		private bool M;
		private bool N;
		private bool O;
		private bool P;
		private bool Q;
		private bool R;
		private bool S;
		private bool T;
		private bool U;
		private bool V;
		private bool W;
		private bool Plus;
		private bool Minus;
		private bool Space;
		private bool X;
		private bool Y;
		private bool Z;
		private bool One;
		private bool Two;
		private bool Three;
		private bool Four;
		private bool Five;
		private bool Six;
		private bool Seven;
		private bool Eight;
		private bool Nine;
		private bool Zero;
		private bool Escape;
		private bool Cancel;
		private bool Enter;
		private bool Shift;
		private bool Control;
		private bool CapsLock;
		private bool Pause;
		private bool Alt;
		private bool End;
		private bool Insert;
		private bool Delete;
		private bool Down;
		private bool Left;
		private bool Right;
		private bool Up;

		private static bool GetAsyncKeyState(int value)
		{
			return NativeMethods.GetAsyncKeyState(value);
		}

        internal KeyboardState(int placeHolder)
		{
			A = GetAsyncKeyState((int)Keys.A);
			B = GetAsyncKeyState((int)Keys.B);
			C = GetAsyncKeyState((int)Keys.C);
			D = GetAsyncKeyState((int)Keys.D);
			E = GetAsyncKeyState((int)Keys.E);
			F = GetAsyncKeyState((int)Keys.F);
			G = GetAsyncKeyState((int)Keys.G);
			H = GetAsyncKeyState((int)Keys.H);
			I = GetAsyncKeyState((int)Keys.I);
			J = GetAsyncKeyState((int)Keys.J);
			K = GetAsyncKeyState((int)Keys.K);
			L = GetAsyncKeyState((int)Keys.L);
			M = GetAsyncKeyState((int)Keys.M);
			N = GetAsyncKeyState((int)Keys.N);
			O = GetAsyncKeyState((int)Keys.O);
			P = GetAsyncKeyState((int)Keys.P);
			Q = GetAsyncKeyState((int)Keys.Q);
			R = GetAsyncKeyState((int)Keys.R);
			S = GetAsyncKeyState((int)Keys.S);
			T = GetAsyncKeyState((int)Keys.T);
			U = GetAsyncKeyState((int)Keys.U);
			V = GetAsyncKeyState((int)Keys.V);
			W = GetAsyncKeyState((int)Keys.W);
			X = GetAsyncKeyState((int)Keys.X);
			Y = GetAsyncKeyState((int)Keys.Y);
			Z = GetAsyncKeyState((int)Keys.Z);
			Space = GetAsyncKeyState((int)Keys.Space);
			One = GetAsyncKeyState((int)Keys.One);
			Two = GetAsyncKeyState((int)Keys.Two);
			Three = GetAsyncKeyState((int)Keys.Three);
			Four = GetAsyncKeyState((int)Keys.Four);
			Five = GetAsyncKeyState((int)Keys.Five);
			Six = GetAsyncKeyState((int)Keys.Six);
			Seven = GetAsyncKeyState((int)Keys.Seven);
			Eight = GetAsyncKeyState((int)Keys.Eight);
			Nine = GetAsyncKeyState((int)Keys.Nine);
			Zero = GetAsyncKeyState((int)Keys.Zero);
			Plus = GetAsyncKeyState((int)Keys.Plus);
			Minus = GetAsyncKeyState((int)Keys.Minus);
			Escape = GetAsyncKeyState((int)Keys.Escape);
			Cancel = GetAsyncKeyState((int)Keys.Cancel);
			Enter = GetAsyncKeyState((int)Keys.Enter);
			Shift = GetAsyncKeyState((int)Keys.Shift);
			Control = GetAsyncKeyState((int)Keys.Control);
			CapsLock = GetAsyncKeyState((int)Keys.CapsLock);
			Pause = GetAsyncKeyState((int)Keys.Pause);
			Alt = GetAsyncKeyState((int)Keys.Alt);
			End = GetAsyncKeyState((int)Keys.End);
			Insert = GetAsyncKeyState((int)Keys.Insert);
			Delete = GetAsyncKeyState((int)Keys.Delete);
			Down = GetAsyncKeyState((int)Keys.Down);
			Left = GetAsyncKeyState((int)Keys.Left);
			Right = GetAsyncKeyState((int)Keys.Right);
			Up = GetAsyncKeyState((int)Keys.Up);
		}
        
		public bool IsKeyDown(Keys key)
		{
			switch (key)
			{
				case Keys.Alt:
					return Alt;
				case Keys.CapsLock:
					return CapsLock;
				case Keys.Cancel:
					return this.Cancel;
				case Keys.Enter:
					return this.Enter;
				case Keys.Shift:
					return this.Shift;
				case Keys.Control:
					return this.Control;
				case Keys.Pause:
					return this.Pause;
				case Keys.Escape:
					return this.Escape;
				case Keys.Space:
					return this.Space;
				case Keys.End:
					return this.End;
				case Keys.Left:
					return this.Left;
				case Keys.Up:
					return this.Up;
				case Keys.Right:
					return this.Right;
				case Keys.Down:
					return this.Down;
				case Keys.Insert:
					return this.Insert;
				case Keys.Delete:
					return this.Delete;
				case Keys.Zero:
					return this.Zero;
				case Keys.One:
					return this.One;
				case Keys.Two:
					return this.Two;
				case Keys.Three:
					return this.Three;
				case Keys.Four:
					return this.Four;
				case Keys.Five:
					return this.Five;
				case Keys.Six:
					return this.Six;
				case Keys.Seven:
					return this.Seven;
				case Keys.Eight:
					return this.Eight;
				case Keys.Nine:
					return this.Nine;
				case Keys.A:
					return this.A;
				case Keys.B:
					return this.B;
				case Keys.C:
					return this.C;
				case Keys.D:
					return this.D;
				case Keys.E:
					return this.E;
				case Keys.F:
					return this.F;
				case Keys.G:
					return this.G;
				case Keys.H:
					return this.H;
				case Keys.I:
					return this.I;
				case Keys.J:
					return this.J;
				case Keys.K:
					return this.K;
				case Keys.L:
					return this.L;
				case Keys.M:
					return this.M;
				case Keys.N:
					return this.N;
				case Keys.O:
					return this.O;
				case Keys.P:
					return this.P;
				case Keys.Q:
					return this.Q;
				case Keys.R:
					return this.R;
				case Keys.S:
					return this.S;
				case Keys.T:
					return this.T;
				case Keys.U:
					return this.U;
				case Keys.V:
					return this.V;
				case Keys.W:
					return this.W;
				case Keys.X:
					return this.X;
				case Keys.Y:
					return this.Y;
				case Keys.Z:
					return this.Z;
				case Keys.Plus:
					return this.Plus;
				case Keys.Minus:
					return this.Minus;
			}
			return false;
		}

		public bool IsKeyUp(Keys key)
		{
			return !(IsKeyDown(key));
		}
		
		#region Equals and GetHashCode implementation
		public override int GetHashCode()
		{
			int hashCode = 0;
				unchecked {
					hashCode += 1000000007 * A.GetHashCode();
					hashCode += 1000000009 * B.GetHashCode();
					hashCode += 1000000021 * C.GetHashCode();
					hashCode += 1000000033 * D.GetHashCode();
					hashCode += 1000000087 * E.GetHashCode();
					hashCode += 1000000093 * F.GetHashCode();
					hashCode += 1000000097 * G.GetHashCode();
					hashCode += 1000000103 * H.GetHashCode();
					hashCode += 1000000123 * I.GetHashCode();
					hashCode += 1000000181 * J.GetHashCode();
					hashCode += 1000000207 * K.GetHashCode();
					hashCode += 1000000223 * L.GetHashCode();
					hashCode += 1000000241 * M.GetHashCode();
					hashCode += 1000000271 * N.GetHashCode();
					hashCode += 1000000289 * O.GetHashCode();
					hashCode += 1000000297 * P.GetHashCode();
					hashCode += 1000000321 * Q.GetHashCode();
					hashCode += 1000000349 * R.GetHashCode();
					hashCode += 1000000363 * S.GetHashCode();
					hashCode += 1000000403 * T.GetHashCode();
					hashCode += 1000000409 * U.GetHashCode();
					hashCode += 1000000411 * V.GetHashCode();
					hashCode += 1000000427 * W.GetHashCode();
					hashCode += 1000000433 * Plus.GetHashCode();
					hashCode += 1000000439 * Minus.GetHashCode();
					hashCode += 1000000447 * Space.GetHashCode();
					hashCode += 1000000453 * X.GetHashCode();
					hashCode += 1000000459 * Y.GetHashCode();
					hashCode += 1000000483 * Z.GetHashCode();
					hashCode += 1000000513 * One.GetHashCode();
					hashCode += 1000000531 * Two.GetHashCode();
					hashCode += 1000000579 * Three.GetHashCode();
					hashCode += 1000000007 * Four.GetHashCode();
					hashCode += 1000000009 * Five.GetHashCode();
					hashCode += 1000000021 * Six.GetHashCode();
					hashCode += 1000000033 * Seven.GetHashCode();
					hashCode += 1000000087 * Eight.GetHashCode();
					hashCode += 1000000093 * Nine.GetHashCode();
					hashCode += 1000000097 * Zero.GetHashCode();
					hashCode += 1000000103 * Escape.GetHashCode();
					hashCode += 1000000123 * Cancel.GetHashCode();
					hashCode += 1000000181 * Enter.GetHashCode();
					hashCode += 1000000207 * Shift.GetHashCode();
					hashCode += 1000000223 * Control.GetHashCode();
					hashCode += 1000000241 * CapsLock.GetHashCode();
					hashCode += 1000000271 * Pause.GetHashCode();
					hashCode += 1000000289 * Alt.GetHashCode();
					hashCode += 1000000297 * End.GetHashCode();
					hashCode += 1000000321 * Insert.GetHashCode();
					hashCode += 1000000349 * Delete.GetHashCode();
					hashCode += 1000000363 * Down.GetHashCode();
					hashCode += 1000000403 * Left.GetHashCode();
					hashCode += 1000000409 * Right.GetHashCode();
					hashCode += 1000000411 * Up.GetHashCode();
				}
					return hashCode;
		}

		public override bool Equals(object obj)
		{
			return (obj is KeyboardState) && Equals((KeyboardState)obj);
		}

		public bool Equals(KeyboardState other)
		{
			return this.A == other.A && this.B == other.B && this.C == other.C && this.D == other.D && this.E == other.E && this.F == other.F && this.G == other.G && this.H == other.H && this.I == other.I && this.J == other.J && this.K == other.K && this.L == other.L && this.M == other.M && this.N == other.N && this.O == other.O && this.P == other.P && this.Q == other.Q && this.R == other.R && this.S == other.S && this.T == other.T && this.U == other.U && this.V == other.V && this.W == other.W && this.Plus == other.Plus && this.Minus == other.Minus && this.Space == other.Space && this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.One == other.One && this.Two == other.Two && this.Three == other.Three && this.Four == other.Four && this.Five == other.Five && this.Six == other.Six && this.Seven == other.Seven && this.Eight == other.Eight && this.Nine == other.Nine && this.Zero == other.Zero && this.Escape == other.Escape && this.Cancel == other.Cancel && this.Enter == other.Enter && this.Shift == other.Shift && this.Control == other.Control && this.CapsLock == other.CapsLock && this.Pause == other.Pause && this.Alt == other.Alt && this.End == other.End && this.Insert == other.Insert && this.Delete == other.Delete && this.Down == other.Down && this.Left == other.Left && this.Right == other.Right && this.Up == other.Up;
		}

		public static bool operator ==(KeyboardState lhs, KeyboardState rhs) {
			return lhs.Equals(rhs);
		}

		public static bool operator !=(KeyboardState lhs, KeyboardState rhs) {
			return !(lhs == rhs);
		}

		#endregion
	}
}