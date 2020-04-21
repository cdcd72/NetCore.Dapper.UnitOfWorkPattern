using System;

namespace Web.Core.Configuration
{
    public interface ISettingsValidator
    {
        /// <summary>
        /// 嘗試驗證 appsettings.json 內 DbSettings 區段
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="validationExceptions"></param>
        /// <returns></returns>
        bool TryValidate(IDbSettingsStructure settings, out AggregateException validationExceptions);
    }
}