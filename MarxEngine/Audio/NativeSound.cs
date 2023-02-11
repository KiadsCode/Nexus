/*
 * Создано в SharpDevelop.
 * Пользователь: Acer
 * Дата: 07.02.2023
 * Время: 20:34
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;

namespace Nexus.Framework.Audio
{
    internal class NativeSound : Component, ISerializable
    {
        public NativeSound()
        {
            sound_location = string.Empty;
        }

        public NativeSound(Stream stream) : this()
        {
            audiostream = stream;
        }

        public NativeSound(string soundLocation) : this()
        {
            if (soundLocation == null) throw new ArgumentNullException("soundLocation");
            sound_location = soundLocation;
        }

        protected NativeSound(SerializationInfo serializationInfo, StreamingContext context) : this()
        {
            throw new NotImplementedException();
        }

        private void LoadFromStream(Stream s)
        {
            mstream = new MemoryStream();
            var buffer = new byte[4096];
            int count;
            while ((count = s.Read(buffer, 0, 4096)) > 0) mstream.Write(buffer, 0, count);
            mstream.Position = 0L;
        }

        private void LoadFromUri(string location)
        {
            mstream = null;
            if (string.IsNullOrEmpty(location)) return;
            Stream stream;
            if (File.Exists(location))
                stream = new FileStream(location, FileMode.Open, FileAccess.Read, FileShare.Read);
            else
                stream = WebRequest.Create(location).GetResponse().GetResponseStream();
            using (stream)
            {
                LoadFromStream(stream);
            }
        }

        public void Load()
        {
            if (IsLoadCompleted) return;
            if (audiostream != null)
                LoadFromStream(audiostream);
            else
                LoadFromUri(sound_location);
            adata = null;
            adev = null;
            IsLoadCompleted = true;
            var e = new AsyncCompletedEventArgs(null, false, this);
            OnLoadCompleted(e);
            if (LoadCompleted != null) LoadCompleted(this, e);
            if (use_win32_player)
            {
                if (win32_player == null)
                {
                    win32_player = new Win32SoundPlayer(mstream);
                    return;
                }

                win32_player.Stream = mstream;
            }
        }

        private void AsyncFinished(IAsyncResult ar)
        {
            (ar.AsyncState as ThreadStart).EndInvoke(ar);
        }

        public void LoadAsync()
        {
            if (IsLoadCompleted) return;
            ThreadStart threadStart = Load;
            threadStart.BeginInvoke(AsyncFinished, threadStart);
        }

        protected virtual void OnLoadCompleted(AsyncCompletedEventArgs e)
        {
        }

        protected virtual void OnSoundLocationChanged(EventArgs e)
        {
        }

        protected virtual void OnStreamChanged(EventArgs e)
        {
        }

        private void Start()
        {
            if (!use_win32_player)
            {
                stopped = false;
                if (adata != null) adata.IsStopped = false;
            }

            if (!IsLoadCompleted) Load();
        }

        public void Play()
        {
            if (!use_win32_player)
            {
                ThreadStart threadStart = PlaySync;
                threadStart.BeginInvoke(AsyncFinished, threadStart);
                return;
            }

            Start();
            if (mstream == null)
            {
                SystemSounds.Beep.Play();
                return;
            }

            win32_player.Play();
        }

        private void PlayLoop()
        {
            Start();
            if (mstream == null)
            {
                SystemSounds.Beep.Play();
                return;
            }

            while (!stopped) PlaySync();
        }

        public void PlayLooping()
        {
            if (!use_win32_player)
            {
                ThreadStart threadStart = PlayLoop;
                threadStart.BeginInvoke(AsyncFinished, threadStart);
                return;
            }

            Start();
            if (mstream == null)
            {
                SystemSounds.Beep.Play();
                return;
            }

            win32_player.PlayLooping();
        }

        public void PlaySync()
        {
            Start();
            if (mstream == null)
            {
                SystemSounds.Beep.Play();
                return;
            }

            if (!use_win32_player)
                try
                {
                    if (adata == null) adata = new WavData(mstream);
                    if (adev == null) adev = AudioDevice.CreateDevice(null);
                    if (adata != null)
                    {
                        adata.Setup(adev);
                        adata.Play(adev);
                    }

                    return;
                }
                catch
                {
                    return;
                }

            win32_player.PlaySync();
        }

        public void Stop()
        {
            if (!use_win32_player)
            {
                stopped = true;
                if (adata != null) adata.IsStopped = true;
            }
            else
            {
                win32_player.Stop();
            }
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        public bool IsLoadCompleted { get; private set; }

        public int LoadTimeout
        {
            get => load_timeout;
            set
            {
                if (value < 0) throw new ArgumentException("timeout must be >= 0");
                load_timeout = value;
            }
        }

        public string SoundLocation
        {
            get => sound_location;
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                sound_location = value;
                IsLoadCompleted = false;
                OnSoundLocationChanged(EventArgs.Empty);
                if (SoundLocationChanged != null) SoundLocationChanged(this, EventArgs.Empty);
            }
        }

        public Stream Stream
        {
            get => audiostream;
            set
            {
                if (audiostream != value)
                {
                    audiostream = value;
                    IsLoadCompleted = false;
                    OnStreamChanged(EventArgs.Empty);
                    if (StreamChanged != null) StreamChanged(this, EventArgs.Empty);
                }
            }
        }

        public object Tag { get; set; } = string.Empty;

        public event AsyncCompletedEventHandler LoadCompleted;

        public event EventHandler SoundLocationChanged;

        public event EventHandler StreamChanged;

        private string sound_location;

        private Stream audiostream;

        private MemoryStream mstream;

        private int load_timeout = 10000;

        private AudioDevice adev;

        private AudioData adata;

        private bool stopped;

        private Win32SoundPlayer win32_player;

        private static readonly bool use_win32_player = Environment.OSVersion.Platform != PlatformID.Unix;
    }
}