using System;
using System.IO;

namespace Nexus.Framework.Audio
{
    internal class WavData : AudioData
    {
        public WavData(Stream data)
        {
            stream = data;
            var array = new byte[44];
            var num = stream.Read(array, 0, 12);
            if (num != 12 || array[0] != 82 || array[1] != 73 || array[2] != 70 || array[3] != 70 || array[8] != 87 ||
                array[9] != 65 || array[10] != 86 || array[11] != 69) throw new Exception("incorrect format" + num);
            num = stream.Read(array, 0, 8);
            if (num != 8 || array[0] != 102 || array[1] != 109 || array[2] != 116 || array[3] != 32)
                throw new Exception("incorrect format (fmt)");
            int num2 = array[4];
            num2 |= array[5] << 8;
            num2 |= array[6] << 16;
            num2 |= array[7] << 24;
            num = stream.Read(array, 0, num2);
            if (num2 != num)
                throw new Exception(string.Concat("Error: Can't Read ", num2, " bytes from stream (", num,
                    " bytes read"));
            var num3 = 0;
            if ((array[num3++] | (array[num3++] << 8)) != 1) throw new Exception("incorrect format (not PCM)");
            channels = (short)(array[num3++] | (array[num3++] << 8));
            sample_rate = array[num3++];
            sample_rate |= array[num3++] << 8;
            sample_rate |= array[num3++] << 16;
            sample_rate |= array[num3++] << 24;
            var num4 = array[num3++] | (array[num3++] << 8) | (array[num3++] << 16);
            var b = array[num3++];
            num3 += 2;
            var num5 = array[num3++] | (array[num3++] << 8);
            if (num5 != 8)
            {
                if (num5 != 16) throw new Exception("bits per sample");
                frame_divider = 2;
                Format = AudioFormat.S16_LE;
            }
            else
            {
                frame_divider = 1;
                Format = AudioFormat.U8;
            }

            num = stream.Read(array, 0, 8);
            if (num != 8) return;
            if (array[0] == 102 && array[1] == 97 && array[2] == 99 && array[3] == 116)
            {
                int num6 = array[4];
                num6 |= array[5] << 8;
                num6 |= array[6] << 16;
                num6 |= array[7] << 24;
                num = stream.Read(array, 0, num6);
                num = stream.Read(array, 0, 8);
            }

            if (array[0] == 100 && array[1] == 97 && array[2] == 116 && array[3] == 97)
            {
                int num7 = array[4];
                num7 |= array[5] << 8;
                num7 |= array[6] << 16;
                num7 |= array[7] << 24;
                data_len = num7;
                data_offset = stream.Position;
                return;
            }

            throw new Exception("incorrect format (data/fact chunck)");
        }

        public override void Play(AudioDevice dev)
        {
            var num = 0;
            var chunkSize = (int)dev.ChunkSize;
            var num2 = data_len;
            var array = new byte[data_len];
            var array2 = new byte[chunkSize];
            stream.Position = data_offset;
            stream.Read(array, 0, data_len);
            while (!IsStopped && num2 >= 0)
            {
                Buffer.BlockCopy(array, num, array2, 0, chunkSize);
                var num3 = dev.PlaySample(array2, chunkSize / (frame_divider * (ushort)channels));
                if (num3 > 0)
                {
                    num += num3 * frame_divider * channels;
                    num2 -= num3 * frame_divider * channels;
                }
            }
        }

        public override int Channels => channels;

        public override int Rate => sample_rate;

        public override AudioFormat Format { get; }

        private readonly Stream stream;

        private readonly short channels;

        private readonly ushort frame_divider;

        private readonly int sample_rate;

        private readonly int data_len;

        private readonly long data_offset;
    }
}