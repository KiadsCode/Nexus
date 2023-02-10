/*
 * Создано в SharpDevelop.
 * Пользователь: Acer
 * Дата: 07.02.2023
 * Время: 14:54
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;

namespace Nexus.Framework.Input
{
	/// <summary>
	/// Description of Mouse.
	/// </summary>
	public static class Mouse
	{
		public static MouseState GetState()
		{
			return new MouseState(0);
		}
		public static void SetPosition(int x, int y)
		{
			NativeMethods.SetCursorPos(x, y);
		}
	}
}
