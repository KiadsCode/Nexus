using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Nexus.Framework.Audio
{
    internal class Win32SoundPlayer : IDisposable
    {
        public Win32SoundPlayer(Stream s)
        {
            if (s != null)
            {
                _buffer = new byte[s.Length];
                s.Read(_buffer, 0, _buffer.Length);
                return;
            }

            _buffer = new byte[0];
        }

        [DllImport("winmm.dll", SetLastError = true)]
        private static extern bool PlaySound(byte[] ptrToSound, UIntPtr hmod, SoundFlags flags);

        public Stream Stream
        {
            set
            {
                Stop();
                if (value != null)
                {
                    _buffer = new byte[value.Length];
                    value.Read(_buffer, 0, _buffer.Length);
                    return;
                }

                _buffer = new byte[0];
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Win32SoundPlayer()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                Stop();
                _disposed = true;
            }
        }

        public void Play()
        {
            PlaySound(_buffer, UIntPtr.Zero, (SoundFlags)5U);
        }

        public void PlayLooping()
        {
            PlaySound(_buffer, UIntPtr.Zero, (SoundFlags)13U);
        }

        public void PlaySync()
        {
            PlaySound(_buffer, UIntPtr.Zero, (SoundFlags)6U);
        }

        public void Stop()
        {
            PlaySound(null, UIntPtr.Zero, SoundFlags.SND_SYNC);
        }

        private byte[] _buffer;

        private bool _disposed;

        private enum SoundFlags : uint
        {
            SND_SYNC,
            SND_ASYNC,
            SND_NODEFAULT,
            SND_MEMORY = 4U,
            SND_LOOP = 8U,
            SND_FILENAME = 131072U
        }
    }
}