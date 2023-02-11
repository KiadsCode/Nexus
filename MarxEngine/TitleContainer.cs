using System;
using System.Globalization;
using System.IO;

namespace Nexus.Framework
{
    public static class TitleContainer
    {
        private static readonly char[] _badCharacters = { ':', '*', '?', '"', '<', '>', '|' };

        public static Stream OpenStream(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            name = GetCleanPath(name);
            try
            {
                var uriString = name.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                new Uri(uriString, UriKind.Relative);
            }
            catch (Exception innerException)
            {
                throw new ArgumentException("Invalid project catalogue", innerException);
            }

            Stream result;

            try
            {
                var path = Path.Combine(TitleLocation._Path, name);
                result = File.OpenRead(path);
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException || ex is DirectoryNotFoundException || ex is ArgumentException)
                    throw new FileNotFoundException(string.Format(CultureInfo.CurrentCulture, "Stream not found",
                        name));
                throw new InvalidOperationException(
                    string.Format(CultureInfo.CurrentCulture, "Error occured while opening stream", name), ex);
            }

            return result;
        }

        internal static bool IsPathAbsolute(string path)
        {
            path = GetCleanPath(path);
            return IsCleanPathAbsolute(path);
        }

        internal static string GetCleanPath(string path)
        {
            path = path.Replace('/', '\\');
            path = path.Replace("\\.\\", "\\");
            while (path.StartsWith(".\\")) path = path.Substring(".\\".Length);
            while (path.EndsWith("\\."))
                if (path.Length > "\\.".Length)
                    path = path.Substring(0, path.Length - "\\.".Length);
                else
                    path = "\\";
            for (var i = 1; i < path.Length; i = CollapseParentDirectory(ref path, i, "\\..\\".Length))
            {
                i = path.IndexOf("\\..\\", i);
                if (i < 0) break;
            }

            if (path.EndsWith("\\.."))
            {
                var i = path.Length - "\\..".Length;
                if (i > 0) CollapseParentDirectory(ref path, i, "\\..".Length);
            }

            if (path == ".") path = string.Empty;
            return path;
        }

        private static int CollapseParentDirectory(ref string path, int position, int removeLength)
        {
            var num = path.LastIndexOf('\\', position - 1) + 1;
            path = path.Remove(num, position - num + removeLength);
            return Math.Max(num - 1, 1);
        }

        private static bool IsCleanPathAbsolute(string path)
        {
            return path.IndexOfAny(_badCharacters) >= 0 || path.StartsWith("\\") || path.StartsWith("..\\") ||
                   path.Contains("\\..\\") || path.EndsWith("\\..") || path == "..";
        }
    }
}