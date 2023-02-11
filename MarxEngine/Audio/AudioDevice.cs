﻿namespace Nexus.Framework.Audio
{
    internal class AudioDevice
    {
        private static AudioDevice TryAlsa(string name)
        {
            AudioDevice result;
            try
            {
                result = new AlsaDevice(name);
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public static AudioDevice CreateDevice(string name)
        {
            var audioDevice = TryAlsa(name);
            if (audioDevice == null) audioDevice = new AudioDevice();
            return audioDevice;
        }

        public virtual bool SetFormat(AudioFormat format, int channels, int rate)
        {
            return true;
        }

        public virtual int PlaySample(byte[] buffer, int num_frames)
        {
            return num_frames;
        }

        public virtual int XRunRecovery(int err)
        {
            return err;
        }

        public virtual void Wait()
        {
        }

        public uint ChunkSize => chunk_size;

        protected uint chunk_size;
    }
}