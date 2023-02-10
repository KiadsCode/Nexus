using System;
using System.Globalization;
using System.IO;

namespace Nexus.Framework
{
    public static class TitleContainer
    {
        private static char[] _badCharacters = new char[] {	':', '*', '?', '"', '<', '>', '|' };
        public static Stream OpenStream(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            name = TitleContainer.GetCleanPath(name);
            try
            {
                string uriString = name.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                new Uri(uriString, UriKind.Relative);
            } catch (Exception innerException)
            {
                throw new ArgumentException("Invalid project catalogue", innerException);
            }
            Stream result;
            
            try
            {
                string path = Path.Combine(TitleLocation._Path, name);
                result = File.OpenRead(path);
            } catch (Exception ex)
            {
                if (ex is FileNotFoundException || ex is DirectoryNotFoundException || ex is ArgumentException)
                {
                    throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture, "Stream not found", new object[]
					{
						name
					}));
                }
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Error occured while opening stream", new object[]
				{
					name
				}), ex);
            }
            return result;
        }
        internal static bool IsPathAbsolute(string path)
        {
            path = TitleContainer.GetCleanPath(path);
            return TitleContainer.IsCleanPathAbsolute(path);
        }
        internal static string GetCleanPath(string path)
        {
            path = path.Replace('/', '\\');
            path = path.Replace("\\.\\", "\\");
            while (path.StartsWith(".\\"))
            {
                path = path.Substring(".\\".Length);
            }
            while (path.EndsWith("\\."))
            {
                if (path.Length > "\\.".Length)
                {
                    path = path.Substring(0, path.Length - "\\.".Length);
                } else
                {
                    path = "\\";
                }
            }
            for (int i = 1; i < path.Length; i = TitleContainer.CollapseParentDirectory(ref path, i, "\\..\\".Length))
            {
                i = path.IndexOf("\\..\\", i);
                if (i < 0)
                {
                    break;
                }
            }
            if (path.EndsWith("\\.."))
            {
                int i = path.Length - "\\..".Length;
                if (i > 0)
                {
                    TitleContainer.CollapseParentDirectory(ref path, i, "\\..".Length);
                }
            }
            if (path == ".")
            {
                path = string.Empty;
            }
            return path;
        }
        private static int CollapseParentDirectory(ref string path, int position, int removeLength)
        {
            int num = path.LastIndexOf('\\', position - 1) + 1;
            path = path.Remove(num, position - num + removeLength);
            return Math.Max(num - 1, 1);
        }
        private static bool IsCleanPathAbsolute(string path)
        {
            return path.IndexOfAny(TitleContainer._badCharacters) >= 0 || path.StartsWith("\\") || (path.StartsWith("..\\") || path.Contains("\\..\\") || path.EndsWith("\\..") || path == "..");
        }
    }
}