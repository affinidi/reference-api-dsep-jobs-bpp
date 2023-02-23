using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace bpp
{
    public static class EnvironmentVariables
    {
        public const string SEARCHURL = "searchUrl";
        public const string SEARCHBASEURL = "searchbaseUrl";
        public const string SAVE_XINPUT_URL = "save_xinput_url";
        public const string BPP_PRIVATEKEY = "bpp_privatekey";
        public const string BPP_SUBSCRIBER_ID = "bpp_subscriber_id";
        public const string BPP_UNIQUE_KEY_ID = "bpp_unique_key_id";
        public const string BPP_URL = "bpp_url";
        public const string BPP_XINPUT_URL = "bpp_Xinput_url";
        public const string VERIFY_SIGNATURE = "verify_signature";

        public static string DSEP_REGISTRY_URL = "dsep_registry_url";

        public static string VERIFY_PROXY_SIGNATURE = "verify_proxy_signature";

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