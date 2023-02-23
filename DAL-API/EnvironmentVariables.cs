using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace search
{
    public static class EnvironmentVariables
    {
        public const string USER = "user";
        public const string SECURE = "secure";
        public const string OPENSEARCHBASEURL = "opensearchBaseUrl";


        public static List<string> GetFields()
        {
            var fields = typeof(EnvironmentVariables).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(f => f.IsLiteral && !f.IsInitOnly)
                .Select(f => f.Name)
                .ToList();
            return fields;
        }
    }
}