﻿using System;
using System.Runtime.InteropServices;

namespace Nexus.Framework.Audio
{
    internal class AlsaDevice : AudioDevice, IDisposable
    {
        [DllImport("libasound")]
        private static extern int snd_pcm_open(ref IntPtr handle, string pcm_name, int stream, int mode);

        [DllImport("libasound")]
        private static extern int snd_pcm_close(IntPtr handle);

        [DllImport("libasound")]
        private static extern int snd_pcm_drain(IntPtr handle);

        [DllImport("libasound")]
        private static extern int snd_pcm_writei(IntPtr handle, byte[] buf, int size);

        [DllImport("libasound")]
        private static extern int snd_pcm_set_params(IntPtr handle, int format, int access, int channels, int rate,
            int soft_resample, int latency);

        [DllImport("libasound")]
        private static extern int snd_pcm_state(IntPtr handle);

        [DllImport("libasound")]
        private static extern int snd_pcm_prepare(IntPtr handle);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params(IntPtr handle, IntPtr param);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_malloc(ref IntPtr param);

        [DllImport("libasound")]
        private static extern void snd_pcm_hw_params_free(IntPtr param);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_any(IntPtr handle, IntPtr param);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_set_access(IntPtr handle, IntPtr param, int access);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_set_format(IntPtr handle, IntPtr param, int format);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_set_channels(IntPtr handle, IntPtr param, uint channel);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_set_rate_near(IntPtr handle, IntPtr param, ref uint rate,
            ref int dir);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_set_period_time_near(IntPtr handle, IntPtr param, ref uint period,
            ref int dir);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_get_period_size(IntPtr param, ref uint period, ref int dir);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_set_buffer_size_near(IntPtr handle, IntPtr param,
            ref uint buff_size);

        [DllImport("libasound")]
        private static extern int
            snd_pcm_hw_params_get_buffer_time_max(IntPtr param, ref uint buffer_time, ref int dir);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_set_buffer_time_near(IntPtr handle, IntPtr param,
            ref uint BufferTime, ref int dir);

        [DllImport("libasound")]
        private static extern int snd_pcm_hw_params_get_buffer_size(IntPtr param, ref uint BufferSize);

        [DllImport("libasound")]
        private static extern int snd_pcm_sw_params(IntPtr handle, IntPtr param);

        [DllImport("libasound")]
        private static extern int snd_pcm_sw_params_malloc(ref IntPtr param);

        [DllImport("libasound")]
        private static extern void snd_pcm_sw_params_free(IntPtr param);

        [DllImport("libasound")]
        private static extern int snd_pcm_sw_params_current(IntPtr handle, IntPtr param);

        [DllImport("libasound")]
        private static extern int snd_pcm_sw_params_set_avail_min(IntPtr handle, IntPtr param, uint frames);

        [DllImport("libasound")]
        private static extern int snd_pcm_sw_params_set_start_threshold(IntPtr handle, IntPtr param,
            uint StartThreshold);

        public AlsaDevice(string name)
        {
            if (name == null) name = "default";
            var num = snd_pcm_open(ref handle, name, 0, 0);
            if (num < 0) throw new Exception("no open " + num);
        }

        ~AlsaDevice()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (sw_param != IntPtr.Zero) snd_pcm_sw_params_free(sw_param);
            if (hw_param != IntPtr.Zero) snd_pcm_hw_params_free(hw_param);
            if (handle != IntPtr.Zero) snd_pcm_close(handle);
            sw_param = IntPtr.Zero;
            hw_param = IntPtr.Zero;
            handle = IntPtr.Zero;
        }

        public override bool SetFormat(AudioFormat format, int channels, int rate)
        {
            var num = 0U;
            var chunk_size = 0U;
            var startThreshold = 0U;
            var num2 = 0U;
            var num3 = 0;
            var num4 = (uint)rate;
            if (snd_pcm_hw_params_malloc(ref hw_param) == 0)
            {
                snd_pcm_hw_params_any(handle, hw_param);
                snd_pcm_hw_params_set_access(handle, hw_param, 3);
                snd_pcm_hw_params_set_format(handle, hw_param, (int)format);
                snd_pcm_hw_params_set_channels(handle, hw_param, (uint)channels);
                num3 = 0;
                snd_pcm_hw_params_set_rate_near(handle, hw_param, ref num4, ref num3);
                num3 = 0;
                snd_pcm_hw_params_get_buffer_time_max(hw_param, ref num2, ref num3);
                if (num2 > 500000U) num2 = 500000U;
                if (num2 > 0U) num = num2 / 4U;
                num3 = 0;
                snd_pcm_hw_params_set_period_time_near(handle, hw_param, ref num, ref num3);
                num3 = 0;
                snd_pcm_hw_params_set_buffer_time_near(handle, hw_param, ref num2, ref num3);
                snd_pcm_hw_params_get_period_size(hw_param, ref chunk_size, ref num3);
                this.chunk_size = chunk_size;
                snd_pcm_hw_params_get_buffer_size(hw_param, ref startThreshold);
                snd_pcm_hw_params(handle, hw_param);
            }
            else
            {
                Console.WriteLine("failed to alloc Alsa hw param struct");
            }

            var num5 = snd_pcm_sw_params_malloc(ref sw_param);
            if (num5 == 0)
            {
                snd_pcm_sw_params_current(handle, sw_param);
                snd_pcm_sw_params_set_avail_min(handle, sw_param, this.chunk_size);
                snd_pcm_sw_params_set_start_threshold(handle, sw_param, startThreshold);
                snd_pcm_sw_params(handle, sw_param);
            }
            else
            {
                Console.WriteLine("failed to alloc Alsa sw param struct");
            }

            if (hw_param != IntPtr.Zero)
            {
                snd_pcm_hw_params_free(hw_param);
                hw_param = IntPtr.Zero;
            }

            if (sw_param != IntPtr.Zero)
            {
                snd_pcm_sw_params_free(sw_param);
                sw_param = IntPtr.Zero;
            }

            return num5 == 0;
        }

        public override int PlaySample(byte[] buffer, int num_frames)
        {
            int num;
            do
            {
                num = snd_pcm_writei(handle, buffer, num_frames);
                if (num < 0) XRunRecovery(num);
            } while (num < 0);

            return num;
        }

        public override int XRunRecovery(int err)
        {
            var result = 0;
            if (-32 == err) result = snd_pcm_prepare(handle);
            return result;
        }

        public override void Wait()
        {
            snd_pcm_drain(handle);
        }

        private IntPtr handle;

        private IntPtr hw_param;

        private IntPtr sw_param;
    }
}