using System.Collections.Generic;

namespace Web.Core.Configuration
{
    public interface IDbSettingsStructure
    {
        /// <summary>
        /// 連線類型
        /// </summary>
        string ConnectionType { get; }

        /// <summary>
        /// 多個連線字串
        /// </summary>
        Dictionary<string, string> ConnectionStrings { get; }
    }
}
