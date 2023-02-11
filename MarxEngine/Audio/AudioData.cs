namespace Nexus.Framework.Audio
{
    internal abstract class AudioData
    {
        public abstract int Channels { get; }

        public abstract int Rate { get; }

        public abstract AudioFormat Format { get; }

        public virtual void Setup(AudioDevice dev)
        {
            dev.SetFormat(Format, Channels, Rate);
        }

        public abstract void Play(AudioDevice dev);

        public virtual bool IsStopped { get; set; }

        protected const int buffer_size = 4096;
    }
}