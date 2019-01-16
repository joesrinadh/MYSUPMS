using System.Configuration;

namespace SUPMS.Infrastructure.Utilities
{
    public class ConnectionHelper
    {
        public static string GetConnectionString(string key)
        {
            string value = string.Empty;
            try
            {
                value = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            catch
            {
                return null;
            }
            return value;
        }
    }
}
