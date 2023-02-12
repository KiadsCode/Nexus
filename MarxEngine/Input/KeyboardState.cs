using System;

namespace Nexus.Framework.Input
{
	/// <summary>
	///     Provides Input of keyboard
	/// </summary>
	public struct KeyboardState : IEquatable<KeyboardState>
    {
        private readonly bool A;
        private readonly bool B;
        private readonly bool C;
        private readonly bool D;
        private readonly bool E;
        private readonly bool F;
        private readonly bool G;
        private readonly bool H;
        private readonly bool I;
        private readonly bool J;
        private readonly bool K;
        private readonly bool L;
        private readonly bool M;
        private readonly bool N;
        private readonly bool O;
        private readonly bool P;
        private readonly bool Q;
        private readonly bool R;
        private readonly bool S;
        private readonly bool T;
        private readonly bool U;
        private readonly bool V;
        private readonly bool W;
        private readonly bool Plus;
        private readonly bool Minus;
        private readonly bool Space;
        private readonly bool X;
        private readonly bool Y;
        private readonly bool Z;
        private readonly bool One;
        private readonly bool Two;
        private readonly bool Three;
        private readonly bool Four;
        private readonly bool Five;
        private readonly bool Six;
        private readonly bool Seven;
        private readonly bool Eight;
        private readonly bool Nine;
        private readonly bool Zero;
        private readonly bool Escape;
        private readonly bool Cancel;
        private readonly bool Enter;
        private readonly bool Shift;
        private readonly bool Control;
        private readonly bool CapsLock;
        private readonly bool Pause;
        private readonly bool Alt;
        private readonly bool End;
        private readonly bool Insert;
        private readonly bool Delete;
        private readonly bool Down;
        private readonly bool Left;
        private readonly bool Right;
        private readonly bool Up;

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
                    return Cancel;
                case Keys.Enter:
                    return Enter;
                case Keys.Shift:
                    return Shift;
                case Keys.Control:
                    return Control;
                case Keys.Pause:
                    return Pause;
                case Keys.Escape:
                    return Escape;
                case Keys.Space:
                    return Space;
                case Keys.End:
                    return End;
                case Keys.Left:
                    return Left;
                case Keys.Up:
                    return Up;
                case Keys.Right:
                    return Right;
                case Keys.Down:
                    return Down;
                case Keys.Insert:
                    return Insert;
                case Keys.Delete:
                    return Delete;
                case Keys.Zero:
                    return Zero;
                case Keys.One:
                    return One;
                case Keys.Two:
                    return Two;
                case Keys.Three:
                    return Three;
                case Keys.Four:
                    return Four;
                case Keys.Five:
                    return Five;
                case Keys.Six:
                    return Six;
                case Keys.Seven:
                    return Seven;
                case Keys.Eight:
                    return Eight;
                case Keys.Nine:
                    return Nine;
                case Keys.A:
                    return A;
                case Keys.B:
                    return B;
                case Keys.C:
                    return C;
                case Keys.D:
                    return D;
                case Keys.E:
                    return E;
                case Keys.F:
                    return F;
                case Keys.G:
                    return G;
                case Keys.H:
                    return H;
                case Keys.I:
                    return I;
                case Keys.J:
                    return J;
                case Keys.K:
                    return K;
                case Keys.L:
                    return L;
                case Keys.M:
                    return M;
                case Keys.N:
                    return N;
                case Keys.O:
                    return O;
                case Keys.P:
                    return P;
                case Keys.Q:
                    return Q;
                case Keys.R:
                    return R;
                case Keys.S:
                    return S;
                case Keys.T:
                    return T;
                case Keys.U:
                    return U;
                case Keys.V:
                    return V;
                case Keys.W:
                    return W;
                case Keys.X:
                    return X;
                case Keys.Y:
                    return Y;
                case Keys.Z:
                    return Z;
                case Keys.Plus:
                    return Plus;
                case Keys.Minus:
                    return Minus;
                default:
                    return false;
            }
        }

        public bool IsKeyUp(Keys key)
        {
            return !IsKeyDown(key);
        }

        #region Equals and GetHashCode implementation

        public override int GetHashCode()
        {
            var hashCode = 0;
            unchecked
            {
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
            return obj is KeyboardState && Equals((KeyboardState)obj);
        }

        public bool Equals(KeyboardState other)
        {
            return A == other.A && B == other.B && C == other.C && D == other.D && E == other.E && F == other.F &&
                   G == other.G && H == other.H && I == other.I && J == other.J && K == other.K && L == other.L &&
                   M == other.M && N == other.N && O == other.O && P == other.P && Q == other.Q && R == other.R &&
                   S == other.S && T == other.T && U == other.U && V == other.V && W == other.W && Plus == other.Plus &&
                   Minus == other.Minus && Space == other.Space && X == other.X && Y == other.Y && Z == other.Z &&
                   One == other.One && Two == other.Two && Three == other.Three && Four == other.Four &&
                   Five == other.Five && Six == other.Six && Seven == other.Seven && Eight == other.Eight &&
                   Nine == other.Nine && Zero == other.Zero && Escape == other.Escape && Cancel == other.Cancel &&
                   Enter == other.Enter && Shift == other.Shift && Control == other.Control &&
                   CapsLock == other.CapsLock && Pause == other.Pause && Alt == other.Alt && End == other.End &&
                   Insert == other.Insert && Delete == other.Delete && Down == other.Down && Left == other.Left &&
                   Right == other.Right && Up == other.Up;
        }

        public static bool operator ==(KeyboardState lhs, KeyboardState rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(KeyboardState lhs, KeyboardState rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}