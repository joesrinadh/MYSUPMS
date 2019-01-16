using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SUPMS.Infrastructure.Utilities
{
    public class FilePathValidator
    {
        public static bool IsValidPath(string path)
        {
            if (path.Trim() == string.Empty)
            {
                return false;
            }
            Regex objAlphaPattern = new Regex(@"[a-zA-Z0-9\/:.]*$");
            bool isValid = objAlphaPattern.IsMatch(path);
            if (!isValid)
            {
                return false;
            }
            path = path.Replace(@"\\", ":");
            string pathname;
            string filename;
            try
            {
                pathname = Path.GetPathRoot(path);
                filename = Path.GetFileName(path);
            }
            catch
            {
                return false;
            }
            if (pathname.StartsWith(@"\"))
                return false;
            if (filename.Trim() == string.Empty)
            {
                return false;
            }
            if (pathname.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                return false;
            }

            if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                return false;
            }

            return true;
        }
    }

}