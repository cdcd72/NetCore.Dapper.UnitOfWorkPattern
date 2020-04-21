using System.Collections.Generic;

namespace Web.Core.Configuration
{
    public class DbSettings : IDbSettingsStructure
    {
        public string ConnectionType { get; set; }

        public Dictionary<string, string> ConnectionStrings { get; set; } = new Dictionary<string, string>();
    }
}
