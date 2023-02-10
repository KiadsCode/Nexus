using System.IO;
using System.Media;

namespace Nexus.Framework.Audio
{
    public class WavSound
    {
		private SoundPlayer _sound = null;
		private bool _paused = true;
		private bool _isLooped = false;
		
		#region Properties
		public bool IsInitialized
		{
			get
			{
				return _sound.IsLoadCompleted;
			}
		}
		public bool IsLooped
		{
			get
			{
				return _isLooped;
			}
			set
			{
				_isLooped = value;
			}
		}
		public bool Paused
		{
			get
			{
				return _paused;
			}
		}
		#endregion
		
        internal WavSound(Stream stream)
        {
        	_isLooped = false;
        	_sound = new SoundPlayer(stream);
        	_sound.Load();
        }

        public void Stop()
        {
            _sound.Stop();
            _paused = true;
        }

        public void Play()
        {
        	if(_isLooped == false)
        		_sound.Play();
        	else
        		_sound.PlayLooping();
            _paused = false;
        }
        
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			WavSound other = obj as WavSound;
				if (other == null)
					return false;
						return object.Equals(this._sound, other._sound) && this._paused == other._paused && this._isLooped == other._isLooped;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			unchecked {
				if (_sound != null)
					hashCode += 1000000007 * _sound.GetHashCode();
				hashCode += 1000000009 * _paused.GetHashCode();
				hashCode += 1000000021 * _isLooped.GetHashCode();
			}
			return hashCode;
		}

		public static bool operator ==(WavSound lhs, WavSound rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}

		public static bool operator !=(WavSound lhs, WavSound rhs) {
			return !(lhs == rhs);
		}

		#endregion
    }
}