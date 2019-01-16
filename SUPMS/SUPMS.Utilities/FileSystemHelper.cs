using System;
using System.IO;

namespace SUPMS.Infrastructure.Utilities
{
    public static class FileSystemHelper
    {

        public static string GetRandomFileNameWithExtension(string extension)
        {
            return String.Format("{0}.{1}", Path.GetRandomFileName().Replace(".", String.Empty), extension);
        }

        public static void DeleteFiles(string path, string filter)
        {
            string[] files = Directory.GetFiles(path, filter);

            foreach (string file in files)
            {
                File.Delete(Path.Combine(path, file));
            }
        }

    }
}
