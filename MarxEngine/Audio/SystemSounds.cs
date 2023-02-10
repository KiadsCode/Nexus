/*
 * Создано в SharpDevelop.
 * Пользователь: Acer
 * Дата: 07.02.2023
 * Время: 20:49
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace Nexus.Framework.Audio
{
	internal sealed class SystemSounds
	{
		private SystemSounds()
		{
		}

		public static SystemSound Asterisk
		{
			get
			{
				return new SystemSound("Asterisk");
			}
		}

		public static SystemSound Beep
		{
			get
			{
				return new SystemSound("Beep");
			}
		}

		public static SystemSound Exclamation
		{
			get
			{
				return new SystemSound("Exclamation");
			}
		}

		public static SystemSound Hand
		{
			get
			{
				return new SystemSound("Hand");
			}
		}

		public static SystemSound Question
		{
			get
			{
				return new SystemSound("Question");
			}
		}
	}
}
