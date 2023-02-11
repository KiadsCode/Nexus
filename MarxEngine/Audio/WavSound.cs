using System.IO;

namespace Nexus.Framework.Audio
{
    public class WavSound
    {
        private readonly SoundPlayer _sound;

        #region Properties

        public bool IsInitialized => _sound.IsLoadCompleted;

        public bool IsLooped { get; set; }

        public bool Paused { get; private set; } = true;

        #endregion

        internal WavSound(Stream stream)
        {
            IsLooped = false;
            _sound = new SoundPlayer(stream);
            _sound.Load();
        }

        public void Stop()
        {
            _sound.Stop();
            Paused = true;
        }

        public void Play()
        {
            if (IsLooped == false)
                _sound.Play();
            else
                _sound.PlayLooping();
            Paused = false;
        }

        #region Equals and GetHashCode implementation

        public override bool Equals(object obj)
        {
            var other = obj as WavSound;
            if (other == null)
                return false;
            return Equals(_sound, other._sound) && Paused == other.Paused && IsLooped == other.IsLooped;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            unchecked
            {
                if (_sound != null)
                    hashCode += 1000000007 * _sound.GetHashCode();
                hashCode += 1000000009 * Paused.GetHashCode();
                hashCode += 1000000021 * IsLooped.GetHashCode();
            }

            return hashCode;
        }

        public static bool operator ==(WavSound lhs, WavSound rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
                return false;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(WavSound lhs, WavSound rhs)
        {
            return !(lhs == rhs);
        }

        #endregion
    }
}