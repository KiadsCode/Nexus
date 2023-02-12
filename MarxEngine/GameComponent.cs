namespace Nexus.Framework
{
    public class GameComponent
    {
        public bool Enabled { get; protected set; } = true;

        public Game Game { get; }

        public GameComponent(Game game)
        {
            Game = game;
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update()
        {
        }

        #region Equals and GetHashCode implementation

        public override bool Equals(object obj)
        {
            var other = obj as GameComponent;
            if (other == null)
                return false;
            return Equals(Game, other.Game) && Enabled == other.Enabled;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            unchecked
            {
                if (Game != null)
                    hashCode += 1000000007 * Game.GetHashCode();
                hashCode += 1000000009 * Enabled.GetHashCode();
            }

            return hashCode;
        }

        public static bool operator ==(GameComponent lhs, GameComponent rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return false;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(GameComponent lhs, GameComponent rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}