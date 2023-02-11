using System.IO;
using System.Reflection;

namespace Nexus.Framework
{
    internal static class TitleLocation
    {
        private static string _titleLocation;

        public static string _Path
        {
            get
            {
                if (_titleLocation == null)
                {
                    var titleLocation = string.Empty;
                    var assembly = Assembly.GetEntryAssembly();
                    if (assembly == null) assembly = Assembly.GetCallingAssembly();
                    if (assembly != null) titleLocation = Path.GetDirectoryName(assembly.Location);
                    _titleLocation = titleLocation;
                }

                return _titleLocation;
            }
        }
    }
}