/*
 * Создано в SharpDevelop.
 * Пользователь: Acer
 * Дата: 07.02.2023
 * Время: 20:40
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */

using System;
using System.IO;

namespace Nexus.Framework.Audio
{
    internal class SystemSound
    {
        internal SystemSound(string tag)
        {
            resource = typeof(SystemSound).Assembly.GetManifestResourceStream(tag + ".wav");
        }

        public void Play()
        {
            using (var sound = new NativeSound(resource))
            {
                sound.Play();
            }
        }

        internal SystemSound()
        {
            throw new Exception("NotSupportedException");
        }

        private readonly Stream resource;
    }
}