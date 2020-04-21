using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Core.Configuration
{
    public class SettingsValidator : ISettingsValidator
    {
        /// <summary>
        /// 嘗試驗證 appsettings.json 內 DbSettings 區段
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="validationExceptions"></param>
        /// <returns></returns>
        public bool TryValidate(IDbSettingsStructure settings, out AggregateException validationExceptions)
        {
            // 若 appsettings.json 沒有 DbSettings 區段
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            
            var exceptions = new List<Exception>();

            // 若 DbSettings:ConnectionType 沒有特別輸入
            if (string.IsNullOrWhiteSpace(settings.ConnectionType)) 
                exceptions.Add(new ArgumentNullException(nameof(settings.ConnectionType)));

            // 若 DbSettings:ConnectionStrings 沒有至少輸入一串
            if (!settings.ConnectionStrings.Any()) 
                exceptions.Add(new ArgumentNullException(nameof(settings.ConnectionStrings)));
            
            validationExceptions = new AggregateException(exceptions);

            return !exceptions.Any();
        }
    }
}
