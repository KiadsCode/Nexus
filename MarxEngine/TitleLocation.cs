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
                if (TitleLocation._titleLocation == null)
                {
                    string titleLocation = string.Empty;
                    Assembly assembly = Assembly.GetEntryAssembly();
                    if (assembly == null)
                    {
                        assembly = Assembly.GetCallingAssembly();
                    }
                    if (assembly != null)
                    {
                        titleLocation = Path.GetDirectoryName(assembly.Location);
                    }
                    TitleLocation._titleLocation = titleLocation;
                }
                return TitleLocation._titleLocation;
            }
        }
    }
}