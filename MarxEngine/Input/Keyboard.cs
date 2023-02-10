/*
 * Создано в SharpDevelop.
 * Пользователь: Acer
 * Дата: 07.02.2023
 * Время: 14:33
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace Nexus.Framework.Input
{
	/// <summary>
	/// Description of Keyboard.
	/// </summary>
	public static class Keyboard
	{
		public static KeyboardState GetState()
		{
			return new KeyboardState(0);
		}
	}
}
